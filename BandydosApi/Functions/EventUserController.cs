using AutoMapper;
using Azure;
using SportPlanner.Repository.Interfaces;
using System.Net;

namespace BandydosApi.Functions;

public class EventUserController
{
    private readonly IMapper _mapper;
    private readonly ICloudTableClient<EventUser> _eventUserRepository;

    public EventUserController(IMapper mapper, ICloudTableClient<EventUser> eventUserRepository)
    {
        _mapper = mapper;
        _eventUserRepository = eventUserRepository;
    }

    [Function("UpdateEventUser")]
    public async Task<HttpResponseData> UpdateEventUser(
        [HttpTrigger(AuthorizationLevel.Function, "put",
        Route = "event/{eventid}/eventuser")] HttpRequestData req, Guid eventid)
    {
        var requestBody = await req.ReadFromJsonAsync<EventUserDto>();
        var eventUser = _mapper.Map<EventUser>(requestBody);
        eventUser.PartitionKey = eventid.ToString();

        try
        {
            await _eventUserRepository.Client.UpdateEntityAsync(eventUser, ETag.All);
        }
        catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}