using AutoMapper;
using Azure;
using Azure.Data.Tables;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Models;

namespace SportPlanner.Repository.Services
{
    public class EventUserService : IEventUserService
    {
        private readonly IMapper _mapper;
        private readonly ICloudTableClient<EventUser> _repository;
        private readonly IGraphService _graphService;

        public EventUserService(IMapper mapper, ICloudTableClient<EventUser> repository, IGraphService graphService)
        {
            _mapper = mapper;
            _repository = repository;
            _graphService = graphService;
        }

        public async Task<IEnumerable<EventUserDto>> GetFromEventId(string eventId)
        {
            return (await _repository.Client
                .QueryAsync<EventUser>(x => x.PartitionKey == eventId)
                .GetFromResponse())
                .Select(e => _mapper.Map<EventUserDto>(e));
        }

        public async Task<CrudResult> Update(UserDto dto)
        {
            await _graphService.UpdateUser(dto);

            var tasks = new List<Task>();
            await foreach (var user in _repository.Client.QueryAsync<EventUser>(x => x.RowKey == dto.Id))
            {
                user.Name = dto.Name;
                tasks.Add(_repository.Client.UpdateEntityAsync(user, ETag.All));
            }

            await Task.WhenAll(tasks);

            return CrudResult.Ok;
        }

        public async Task<IEnumerable<EventUser>> AddAllUsersToEvent(string eventId)
        {
            var eventUsers = (await _graphService.GetUsers())?.Select(u => _mapper.Map<EventUser>(u));

            var addUserTransaction = eventUsers?.Select(u =>
            {
                u.PartitionKey = eventId;
                return new TableTransactionAction(TableTransactionActionType.Add, u);
            });
            await _repository.Client.SubmitTransactionAsync(addUserTransaction);

            return eventUsers ?? Enumerable.Empty<EventUser>();
        }
    }
}
