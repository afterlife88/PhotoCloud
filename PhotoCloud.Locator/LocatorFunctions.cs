using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhotoCloud.Infrastructure.Utils;
using PhotoCloud.Infrastructure.Utils.Messages;

namespace PhotoCloud.Locator;

public sealed class LocatorFunctions
{

    [Function("extractLocation")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
        HttpRequestData request,
        ILogger log,
        FunctionContext functionContext)
    {
        var locatorRequest = await System.Text.Json.JsonSerializer.DeserializeAsync<LocatorRequest>(request.Body);
        var logger = functionContext.GetLogger<LocatorFunctions>();
        logger.LogInformation("Locator msvc - Request received: Picture blob Url: {BlobUrl}", locatorRequest!.BlobUrl);

        // Simulate lookup for GPS
        Thread.Sleep(2000);

        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new LocatorResponse(new Location(51.5074m, 0.1278m)));

        return response;
    }
    [Function("extractLocationMessage")]
    [ServiceBusOutput("photoslocation", Connection = "ServiceBusConnectionString")]
    public string ExtractLocation(
        [ServiceBusTrigger("photos", "Locator", Connection = "ServiceBusConnectionString")]
        string message,
        FunctionContext functionContext)
    {
        var photoMessage = System.Text.Json.JsonSerializer.Deserialize<PhotoMessage>(message)!;
        var logger = functionContext.GetLogger<LocatorFunctions>();
        logger.LogInformation("Locator msvc - Message received: Picture blob Url: {BlobUrl}", photoMessage!.BlobUrl);

        // Simulate lookup for GPS
        Thread.Sleep(2000);

        var locationMessage = new PhotoLocationMessage(photoMessage.PictureId, new Location(51.5074m, 0.1278m));
        var payload = System.Text.Json.JsonSerializer.Serialize(locationMessage);
        return payload;
    }
}