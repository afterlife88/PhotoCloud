using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader;

public class UploaderPublisher
{
    [Function("uploadAsync")]
    public async Task<HttpResponseData> RunUploadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req,
        ILogger log,
        FunctionContext _)
    {
        var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

        var author = queryDictionary["author"];
        var title = queryDictionary["title"];
        var deviceId = queryDictionary["deviceId"];

        // Simulate blob upload
        Thread.Sleep(1000);
        var blobUrl = $"https://example.com/{Guid.NewGuid()}";

        var serviceBusClient =
            new ServiceBusClient(
                "Endpoint=sb://service-bus-demo-avanade.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rQK4MYlVlFlP3GsKeqg746te62CAqVnwVLHOFvKkDIQ=");

        var sender = serviceBusClient.CreateSender("photos");
        var pictureId = Guid.NewGuid().ToString();
        var photoMessage = new PhotoMessage(blobUrl, title, title, deviceId, pictureId);

        var payload = JsonSerializer.Serialize(photoMessage);

        var message = new ServiceBusMessage(payload);
        await sender.SendMessageAsync(message);

        await sender.CloseAsync();

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(new { blobUrl, pictureId });
        return response;
    }
}