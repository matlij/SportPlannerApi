using SportPlanner.DataLayer.Specifications.Events;

namespace SportPlannerFunctionApi;

public class EventController
{
    private readonly ILogger<EventController> _logger;
    private readonly IRepository<Event> _dataAccess;

    public EventController(ILoggerFactory loggerFactory, IRepository<Event> dataAccess)
    {
        _logger = loggerFactory.CreateLogger<EventController>();
        _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
    }

    [Function("GetAllEvents")]
    public async Task<HttpResponseData> GetAllEvents(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Event")] HttpRequestData req)
    {
        var query = HttpUtility.ParseQueryString(req.Url.Query);
        string userId = query["userId"];
        string limit = query["limit"];
        var limitParsed = string.IsNullOrEmpty(limit) ? 100 : int.Parse(limit);

        var entities = string.IsNullOrEmpty(userId)
            ? await _dataAccess.GetAll<EventDto>(limitParsed)
            : await _dataAccess.Get<EventDto>(new GetEventsByUserIdSpecification(Guid.Parse(userId)), limitParsed);

        return await req.OkObjectResponse(entities);
    }

    [Function("GetEvent")]
    public async Task<HttpResponseData> GetEvent([HttpTrigger(AuthorizationLevel.Function, "get", Route = "event/{id}")] HttpRequestData req, Guid id)
    {
        var entity = (await _dataAccess.Get<EventDto>(new GetEventByIdSpecification(id)))?.SingleOrDefault();

        if (entity is null)
        {
            return req.NotFoundResponse();
        }

        return await req.OkObjectResponse(entity);
    }

    [Function("AddEvent")]
    public async Task<HttpResponseData> AddEvent([HttpTrigger(AuthorizationLevel.Function, "post", Route = "event")] HttpRequestData req)
    {
        var requestBody = await req.ReadFromJsonAsync<EventDto>();
        var (crudResult, _) = await _dataAccess.Add(requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    [Function("UpdateEvent")]
    public async Task<HttpResponseData> UpdateEvent([HttpTrigger(AuthorizationLevel.Function, "put", Route = "event/{id}")] HttpRequestData req, Guid id)
    {
        var requestBody = await req.ReadFromJsonAsync<EventDto>();
        requestBody.Id = id;

        var crudResult = await _dataAccess.Update(new GetEventByIdSpecification(id), requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    [Function("DeleteEvent")]
    public async Task<HttpResponseData> DeleteEvent([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "event/{id}")] HttpRequestData req, Guid id)
    {
        var crudResult = await _dataAccess.Delete(id);

        return req.ToResponse(crudResult, _logger);
    }
}
