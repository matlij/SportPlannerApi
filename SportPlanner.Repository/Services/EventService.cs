using AutoMapper;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using SportPlanner.DataLayer.Models;
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

        public async Task<EventDto?> Get(string partitionKey, string rowKey)
        {
            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentException($"'{nameof(partitionKey)}' cannot be null or empty.", nameof(partitionKey));
            }

            if (string.IsNullOrEmpty(rowKey))
            {
                throw new ArgumentException($"'{nameof(rowKey)}' cannot be null or empty.", nameof(rowKey));
            }

            try
            {
                var response = await _eventRepository.Client.GetEntityAsync<Event>(partitionKey, rowKey);

                return await CreateEventDto(response.Value);
            }
            catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async Task<IEnumerable<EventDto>> GetAll(DateTimeOffset from)
        {
            var events = new List<EventDto>();
            await foreach (var @event in _eventRepository.Client.QueryAsync<Event>(x => x.Date >= from))
            {
                var eventDto = await CreateEventDto(@event);
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

        public async Task<(CrudResult result, EventDto dto)> Update(EventDto entityDto)
        {
            try
            {
                var entity = _mapper.Map<Event>(entityDto);
                await _eventRepository.Client.UpdateEntityAsync(entity, ETag.All);
            }
            catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
            {
                return new(CrudResult.NotFound, entityDto);
            }

            return new(CrudResult.Ok, entityDto);
        }

        private async Task<EventDto> CreateEventDto(Event @event)
        {
            var users = await _eventUserRepository.Client
                .QueryAsync<EventUser>(x => x.PartitionKey == @event.RowKey)
                .GetFromResponse();

            var eventDto = _mapper.Map<EventDto>(@event);
            eventDto.Users = _mapper.Map<IEnumerable<EventUserDto>>(users);
            return eventDto;
        }
    }
}
