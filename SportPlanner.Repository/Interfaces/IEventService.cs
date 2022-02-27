using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.Repository.Interfaces;

public interface IEventService
{
    Task<(CrudResult result, EventDto dto)> Add(EventDto entityDto);
    Task<IEnumerable<EventDto>> GetAll();
    Task<(CrudResult result, EventDto dto)> Update(EventDto entityDto);
}