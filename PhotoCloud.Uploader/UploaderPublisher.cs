using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader;

public class UploaderPublisher
{
    [Function("uploadAsync")]
    public async Task<HttpResponseData> RunUploadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req,
        FunctionContext _)
    {
        var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

        var author = queryDictionary["author"];
        var title = queryDictionary["title"];

        // Simulate blob upload
        Thread.Sleep(1000);
        var blobUrl = $"https://example.com/{Guid.NewGuid()}";
        var pictureId = Guid.NewGuid().ToString();

        var serviceBusClient =
            new ServiceBusClient(
                "Endpoint=sb://service-bus-demo-avanade.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rQK4MYlVlFlP3GsKeqg746te62CAqVnwVLHOFvKkDIQ=");

        var sender = serviceBusClient.CreateSender("photos");
        var photoMessage = new PhotoMessage(blobUrl, author, title, pictureId);
        // var testNewObj 
        var payload = JsonSerializer.Serialize(photoMessage);

        var message = new ServiceBusMessage(payload);
        await sender.SendMessageAsync(message);

        await sender.CloseAsync();

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(new { blobUrl, pictureId });
        return response;
    }
}