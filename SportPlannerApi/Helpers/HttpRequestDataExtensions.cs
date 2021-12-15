using Microsoft.Azure.Functions.Worker.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportPlanner.ModelsDto.Enums;
using Microsoft.Extensions.Logging;

namespace SportPlannerApi.Helpers
{
    public static class HttpRequestDataExtensions
    {
        public static HttpResponseData ToResponse(this HttpRequestData request, CrudResult crudResult, ILogger logger)
        {
            switch (crudResult)
            {
                case CrudResult.Ok:
                    return request.CreateResponse(HttpStatusCode.NoContent);
                case CrudResult.NotFound:
                    return request.CreateResponse(HttpStatusCode.NotFound);
                case CrudResult.AlreadyExists:
                    return request.CreateResponse(HttpStatusCode.Conflict);
                case CrudResult.Error:
                case CrudResult.NoAction:
                case CrudResult.Unknown:
                default:
                    logger.LogError($"{request.Method} request to URL '{request.Url}' failed. Result: {crudResult}. Request body: {request.ReadAsString()}");
                    return request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public static async Task<HttpResponseData> OkObjectResponse<T>(this HttpRequestData request, T data)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(data).ConfigureAwait(false);
            return response;
        }

        public static HttpResponseData NotFoundResponse(this HttpRequestData request) => request.CreateResponse(HttpStatusCode.NotFound);

        public static async Task<HttpResponseData> ValidationResponse(this HttpRequestData request,
            IDictionary<string, string[]> errors,
            string? detail = null,
            string? instance = null,
            int? statusCode = null,
            string? title = null,
            string? type = null)
        {
            var problemDetails = new ValidationProblemDetails(errors)
            {
                Detail = detail,
                Instance = instance,
                Type = type,
                Status = statusCode,
            };

            if (!string.IsNullOrWhiteSpace(title))
            {
                problemDetails.Title = title;
            }

            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(problemDetails).ConfigureAwait(false);
            return response;
        }
    }
}
