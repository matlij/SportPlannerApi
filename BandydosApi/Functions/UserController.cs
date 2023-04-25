using SportPlanner.Repository.Interfaces;
using System.Net;
using System.Text.Json;

namespace BandydosApi.Functions;

public class UserController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILoggerFactory loggerFactory, IUserService userService)
    {
        _logger = loggerFactory.CreateLogger<UserController>();
        _userService = userService;
    }

    //[Function("GetAllUsers")]
    //public async Task<HttpResponseData> GetAllUsers(
    //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "User")] HttpRequestData req)
    //{
    //    var data = await _dataAccess.GetAll<UserDto>();

    //    return await req.OkObjectResponse(data);
    //}

    //[Function("GetUser")]
    //public async Task<HttpResponseData> GetUser([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequestData req, Guid id)
    //{
    //    var entity = (await _dataAccess.Get<UserDto>(new GetByIdSpecification<User>(id)))?.SingleOrDefault();

    //    if (entity is null)
    //    {
    //        return req.NotFoundResponse();
    //    }

    //    return await req.OkObjectResponse(entity);
    //}

    [Function("AddUser")]
    public async Task<HttpResponseData> AddUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequestData req)
    {
        var requestBody = await req.ReadFromJsonAsync<UserDto>();
        if (requestBody is null)
        {
            _logger.LogWarning("Failed to parse request {req}", JsonSerializer.Serialize(req.Body));
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        var (crudResult, _) = await _userService.AddUser(requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    //[Function("UpdateUser")]
    //public async Task<HttpResponseData> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "put", Route = "user/{id}")] HttpRequestData req, Guid id)
    //{
    //    var requestBody = await req.ReadFromJsonAsync<UserDto>();

    //    var crudResult = await _dataAccess.Update(new GetByIdSpecification<User>(id), requestBody);

    //    return req.ToResponse(crudResult, _logger);
    //}

    //[Function("DeleteUser")]
    //public async Task<HttpResponseData> DeleteUser([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{id}")] HttpRequestData req, Guid id)
    //{
    //    var crudResult = await _dataAccess.Delete(id);

    //    return req.ToResponse(crudResult, _logger);
    //}
}
