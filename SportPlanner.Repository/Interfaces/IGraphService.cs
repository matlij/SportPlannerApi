using SportPlanner.ModelsDto;

public interface IGraphService
{
    Task<IEnumerable<UserDto>?> GetUsers();
    Task<IEnumerable<UserDto>?> GetUsersAssignedToServicePrincipal(string principalId);
}