using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.Repository.Interfaces
{
    public interface IUserService
    {
        Task<(CrudResult result, UserDto dto)> AddUser(UserDto user);
    }
}