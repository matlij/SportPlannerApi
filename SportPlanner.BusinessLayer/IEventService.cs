using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.BusinessLayer;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAll();
}