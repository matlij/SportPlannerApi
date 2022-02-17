using AutoMapper;
using Azure;
using Azure.Data.Tables;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Models;
using System.Net;

namespace SportPlanner.Repository.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly ICloudTableClient<Event> _eventRepository;
        private readonly ICloudTableClient<EventUser> _eventUserRepository;

        public EventService(IMapper mapper, ICloudTableClient<Event> eventRepository, ICloudTableClient<EventUser> eventUserRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _eventUserRepository = eventUserRepository;
        }

        public async Task<EventDto> Get<EventDto>(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventDto>> GetAll()
        {
            var events = new List<EventDto>();
            await foreach (var @event in _eventRepository.Client.QueryAsync<Event>())
            {
                var users = await _eventUserRepository.Client.QueryAsync<EventUser>(x => x.PartitionKey == @event.RowKey)
                    .GetFromResponse();

                var eventDto = _mapper.Map<EventDto>(@event);
                eventDto.Users = _mapper.Map<IEnumerable<EventUserDto>>(users);
                events.Add(eventDto);
            }

            return events;
        }

        public Task<CrudResult> Delete(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public async Task<(CrudResult result, EventDto dto)> Add(EventDto entityDto)
        {
            try
            {
                var entity = _mapper.Map<Event>(entityDto);
                await _eventRepository.Client.AddEntityAsync(entity);

                var addUserTransaction = entityDto.Users.Select(u => 
                {
                    var user = _mapper.Map<EventUser>(u);
                    user.PartitionKey = entity.RowKey;
                    return new TableTransactionAction(TableTransactionActionType.Add, user);
                });
                await _eventUserRepository.Client.SubmitTransactionAsync(addUserTransaction);
            }
            catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.Conflict)
            {
                return new(CrudResult.AlreadyExists, entityDto);
            }

            return new(CrudResult.Ok, entityDto);
        }
    }
}
