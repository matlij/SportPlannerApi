using AutoMapper;
using Azure;
using Microsoft.Extensions.Logging;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Models;
using System.Net;

namespace SportPlanner.Repository.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly ICloudTableClient<User> _userRepository;
    private readonly ICloudTableClient<EventUser> _eventRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IMapper mapper, ICloudTableClient<User> userRepository, ICloudTableClient<EventUser> eventRepository, ILogger<UserService> logger)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _eventRepository = eventRepository;
        _logger = logger;
    }

    public async Task<(CrudResult result, UserDto dto)> AddUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        CrudResult crudResult;
        try
        {
            await _userRepository.Client.AddEntityAsync(user);
            crudResult = CrudResult.Ok;
        }
        catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.Conflict)
        {
            crudResult = CrudResult.AlreadyExists;
        }

        await AddUserToFutureEvents(user);

        return new(crudResult, userDto);
    }

    private async Task AddUserToFutureEvents(User user)
    {
        var now = DateTime.Now.Ticks.ToString();
        var events = _eventRepository.Client.QueryAsync<EventUser>(filter: $"PartitionKey gt '{now}'");

        await foreach (var @event in events)
        {
            try
            {
                var eventUser = new EventUser
                {
                    PartitionKey = @event.RowKey,
                    RowKey = user.RowKey,
                    Name = user.Name,
                };
                var response = await _eventRepository.Client.UpsertEntityAsync(eventUser);

                //_logger.LogInformation("Attempt to add user {userId} to Event {eventId} (date {eventdate}) resulted in status {status}", eventUser.RowKey, @event.RowKey, response.Status);
            }
            catch (RequestFailedException e)
            {
                _logger.LogWarning(e, "Add user {userId} to Event {eventId} failed", @event.RowKey, user.RowKey);
            }
        }
    }
}

