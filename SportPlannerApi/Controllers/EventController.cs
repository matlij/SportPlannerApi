using SportPlanner.Repository.Interfaces;

namespace SportPlannerFunctionApi;

public class EventController
{
    private readonly ILogger<EventController> _logger;
    private readonly IEventService _eventService;

    public EventController(ILoggerFactory loggerFactory, IEventService eventService)
    {
        _logger = loggerFactory.CreateLogger<EventController>();
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    }

    [Function("GetAllEvents")]
    public async Task<HttpResponseData> GetAllEvents(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Event")] HttpRequestData req)
    {
        var query = HttpUtility.ParseQueryString(req.Url.Query);
        string userId = query["userId"];

        var from = query["from"];
        if (!DateTimeOffset.TryParse(from, out var fromDate))
        {
            fromDate = new DateTimeOffset(new DateTime(2000, 1, 1));
            _logger.LogDebug($"Failed to parse from date parameter {from}. Settings from date to {fromDate}");
        }

        string limit = query["limit"];
        var limitParsed = string.IsNullOrEmpty(limit) ? 100 : int.Parse(limit);

        var result = await _eventService.GetAll(fromDate);
        return await req.OkObjectResponse(result);
    }

    //[Function("GetEvent")]
    //public async Task<HttpResponseData> GetEvent([HttpTrigger(AuthorizationLevel.Function, "get", Route = "event/{id}")] HttpRequestData req, DateTime id)
    //{
    //    var entity = await _eventService.Get<EventDto>(CloudTableConstants.PartitionKeyEvent, id.Ticks.ToString());

    //    if (entity is null)
    //    {
    //        return req.NotFoundResponse();
    //    }

    //    return await req.OkObjectResponse(entity);
    //}

    [Function("AddEvent")]
    public async Task<HttpResponseData> AddEvent([HttpTrigger(AuthorizationLevel.Function, "post", Route = "event")] HttpRequestData req)
    {
        var requestBody = await req.ReadFromJsonAsync<EventDto>();
        var (crudResult, _) = await _eventService.Add(requestBody);

        return req.ToResponse(crudResult, _logger);
    }

    [Function("UpdateEvent")]
    public async Task<HttpResponseData> UpdateEvent([HttpTrigger(AuthorizationLevel.Function, "put", Route = "event/{id}")] HttpRequestData req, DateTime id)
    {
        var requestBody = await req.ReadFromJsonAsync<EventDto>();
        requestBody.Date = id;

        var result = await _eventService.Update(requestBody);

        return req.ToResponse(result.result, _logger);
    }

    //[Function("DeleteEvent")]
    //public async Task<HttpResponseData> DeleteEvent([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "event/{id}")] HttpRequestData req, DateTime id)
    //{
    //    var crudResult = await _eventService.Delete(CloudTableConstants.PartitionKeyEvent, id.Ticks.ToString());

    //    return req.ToResponse(crudResult, _logger);
    //}
}
