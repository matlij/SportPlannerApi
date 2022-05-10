using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.Repository.Interfaces;

public interface IEventService
{
    Task<(CrudResult result, EventDto dto)> Add(EventDto entityDto);
    Task<EventDto?> Get(string partitionKey, string rowKey);
    Task<IEnumerable<EventDto>> GetAll(DateTimeOffset from);
    Task<(CrudResult result, EventDto dto)> Update(EventDto entityDto);
}