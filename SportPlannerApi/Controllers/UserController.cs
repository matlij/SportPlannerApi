using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SportPlanner.DataLayer;
using SportPlanner.DataLayer.Models;
using SportPlanner.ModelsDto;
using System;
using System.Threading.Tasks;
using SportPlannerApi.Helpers;
using SportPlanner.DataLayer.Specifications;
using System.Linq;

namespace SportPlannerFunctionApi;

public class UserController
{
    private readonly ILogger<UserController> _logger;
    private readonly IRepository<User> _dataAccess;

    public UserController(ILoggerFactory loggerFactory, IRepository<User> dataAccess)
    {
        _logger = loggerFactory.CreateLogger<UserController>();
        _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
    }

    [Function("GetAllUsers")]
    public async Task<HttpResponseData> GetAllUsers(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "User")] HttpRequestData req)
    {
        var data = await _dataAccess.GetAll<UserDto>();

        return await req.OkObjectResponse(data);
    }

    [Function("GetUser")]
    public async Task<HttpResponseData> GetUser([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequestData req, Guid id)
    {
        var entity = (await _dataAccess.Get<UserDto>(new GetUserByIdSpecification(id)))?.SingleOrDefault();

        if (entity is null)
        {
            return req.NotFoundResponse();
        }

        return await req.OkObjectResponse(entity);
    }

    [Function("AddUser")]
    public async Task<HttpResponseData> AddUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequestData req)
    {
        var requestBody = await req.ReadFromJsonAsync<UserDto>();
        var (crudResult, _) = await _dataAccess.Add(requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    [Function("UpdateUser")]
    public async Task<HttpResponseData> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "put", Route = "user/{id}")] HttpRequestData req, Guid id)
    {
        var requestBody = await req.ReadFromJsonAsync<UserDto>();

        var crudResult = await _dataAccess.Update(new GetUserByIdSpecification(id), requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    [Function("DeleteUser")]
    public async Task<HttpResponseData> DeleteUser([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{id}")] HttpRequestData req, Guid id)
    {
        var crudResult = await _dataAccess.Delete(id);

        return req.ToResponse(crudResult, _logger);
    }
}
