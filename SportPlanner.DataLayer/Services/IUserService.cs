using SportPlanner.ModelsDto;
using SportPlanner.ModelsDto.Enums;

namespace SportPlanner.DataLayer.Services
{
    public interface IUserService
    {
        Task<(CrudResult result, UserDto dto)> AddUser(UserDto user);
    }
}