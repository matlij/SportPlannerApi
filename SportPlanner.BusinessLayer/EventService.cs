using AutoMapper;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.Repository;
using SportPlanner.Repository.Models;

namespace SportPlanner.BusinessLayer
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

            //var response = await _client.GetEntityAsync<Event>(partitionKey, rowKey);

            //return _mapper.Map<EventDto>(response.Value);
        }

        public async Task<IEnumerable<EventDto>> GetAll()
        {
            var events = new List<EventDto>();
            await foreach (var @event in _eventRepository.Client.QueryAsync<Event>())
            {
                var users = await _eventUserRepository.Client.QueryAsync<EventUser>(x => x.PartitionKey == @event.RowKey)
                    .GetFromResponse();

                var eventDto =  _mapper.Map<EventDto>(@event);
                eventDto.Users = _mapper.Map<IEnumerable<EventUserDto>>(users);
            }

            return events;
        }

        public Task<CrudResult> Delete(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        public async Task<(CrudResult result, EventDto dto)> Add<EventDto>(EventDto entityDto)
        {
            throw new NotImplementedException();

            //await AddEventUsers(entityDto);
            //return await AddEvent(entityDto);
        }

        //private async Task<(CrudResult result, EventDto dto)> AddEvent(EventDto entityDto)
        //{
        //    var entity = _mapper.Map<Event>(entityDto);
        //    var result = await _eventRepository.Add(entity);
        //    return HandleResponse(entityDto, result);
        //}

        //private async Task AddEventUsers(EventDto entityDto)
        //{
        //    foreach (var userDto in entityDto.Users)
        //    {
        //        var user = _mapper.Map<EventUser>(userDto);
        //        await _eventUserRepository.Add(user);
        //    }
        //}

        //private static (CrudResult result, EventDto dto) HandleResponse(EventDto entityDto, (CrudResult result, Event dto) result)
        //{
        //    if (result.result == CrudResult.AlreadyExists)
        //    {
        //        return (CrudResult.AlreadyExists, entityDto);
        //    }

        //    return (CrudResult.Ok, entityDto);
        //}
    }
}
