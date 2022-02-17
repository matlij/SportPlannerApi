using AutoMapper;
using Microsoft.Extensions.Logging;
using SportPlanner.DataLayer.Models;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.ModelsDto.Extensions;
using SportPlanner.Repository.Interfaces;
using SportPlanner.Repository.Models;
using SportPlanner.Repository.Models.Static;

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
        await _userRepository.Client.AddEntityAsync(user);
        await AddUserToFutureEvents(user);

        return new(CrudResult.Ok, userDto);
    }

    private async Task AddUserToFutureEvents(User user)
    {
        var events = _eventRepository.Client.QueryAsync<EventUser>(e => long.Parse(e.PartitionKey) > DateTime.Now.Ticks);
        await foreach (var @event in events)
        {
            var eventUser = new EventUser
            {
                PartitionKey = @event.RowKey,
                RowKey = user.RowKey,
                UserName = user.Name,
            };
            await _eventRepository.Client.UpsertEntityAsync(eventUser);

            _logger.LogDebug($"Add user to Event {@event.RowKey}");
        }
    }
}

