using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Locator;

public sealed class LocatorHttp
{

    [Function("extractLocation")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
        HttpRequestData request,
        ILogger log,
        FunctionContext functionContext)
    {
        var locatorRequest = await System.Text.Json.JsonSerializer.DeserializeAsync<LocatorRequest>(request.Body);
        var logger = functionContext.GetLogger<LocatorHttp>();
        logger.LogInformation("Locator msvc - Request received: Picture blob Url: {BlobUrl}", locatorRequest!.BlobUrl);

        // Simulate lookup for GPS
        Thread.Sleep(2000);

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new LocatorResponse(new Location(51.5074m, 0.1278m)));

        return response;
    }
}