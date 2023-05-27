using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;
using SportPlanner.Repository.Models;

namespace SportPlanner.Repository.Interfaces
{
    public interface IEventUserService
    {
        Task<IEnumerable<EventUser>> AddAllUsersToEvent(string eventId);
        Task<IEnumerable<EventUserDto>> GetFromEventId(string eventId);
        Task<CrudResult> Update(UserDto dto);
    }
}