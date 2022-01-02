using Microsoft.Extensions.Logging;
using SportPlanner.DataLayer.Models;
using SportPlanner.DataLayer.Specifications;
using SportPlanner.DataLayer.Specifications.Events;
using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.ModelsDto.Extensions;

namespace SportPlanner.DataLayer.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Event> _eventRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IRepository<User> userRepository, IRepository<Event> eventRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _eventRepository = eventRepository;
        _logger = logger;
    }

    public async Task<(CrudResult result, UserDto dto)> AddUser(UserDto user)
    {
        var result = await _userRepository.Add(user, string.IsNullOrEmpty(user.Name) ? null : new GetUserByNameSpecification(user.Name));
        if (result.result.IsPositiveResult())
        {
            await AddUserToFutureEvents(result.dto);
        }

        return result;
    }

    private async Task AddUserToFutureEvents(UserDto user)
    {
        var events = await _eventRepository.Get<EventDto>(new GetOnlyUpcomingEventsSpecification());
        foreach (var @event in events)
        {
            var result = await AddUserToEvent(user, @event);

            _logger.LogDebug($"Add user to Event {@event.Id} result: {result}");
        }
    }

    private Task<CrudResult> AddUserToEvent(UserDto user, EventDto @event)
    {
        if (@event.Users.Any(u => u.UserId == user.Id))
            return Task.FromResult(CrudResult.AlreadyExists);

        var users = @event.Users.ToList();
        users.Add(new EventUserDto()
        {
            UserId = user.Id,
            UserName = user.Name
        });

        @event.Users = users;

        return _eventRepository.Update(new GetEventByIdSpecification(@event.Id), @event);
    }
}

