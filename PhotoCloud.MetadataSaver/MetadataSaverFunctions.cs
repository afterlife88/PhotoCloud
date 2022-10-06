using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.MetadataSaver;

public sealed class MetadataSaverFunctions
{
    [Function("saveMetadata")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
        HttpRequestData request,
        ILogger log,
        FunctionContext functionContext)
    {
        var metadataRequest = await JsonSerializer.DeserializeAsync<MetadataRequest>(request.Body);

        var logger = functionContext.GetLogger<MetadataSaverFunctions>();
        logger.LogInformation("Request received Author: " +
                              "{Author}, Title: {MetadataRequestTitle}, " +
                              "Latitude: {LocationLatitude}, " +
                              "Longitude: {LocationLongitude}",
            metadataRequest!.Author,
            metadataRequest.Title,
            metadataRequest.Location.Latitude,
            metadataRequest.Location.Longitude);

        var id = Guid.NewGuid().ToString();
        // Simulate save
        Thread.Sleep(500);

        var response = request.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(new MetadataResponse(metadataRequest.Title, metadataRequest.Author,
            metadataRequest.Date, metadataRequest.Location, id));

        return response;
    }
}