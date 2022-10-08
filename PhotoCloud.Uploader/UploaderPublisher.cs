using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader;

public class UploaderPublisher
{
    private readonly IConfiguration _configuration;

    public UploaderPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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

        var serviceBusClient = new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnectionString"));

        // TODO: 
        // Find from msdn documentation how to create a message and sends to photos topic
        // You should send a message with the following structure: { "pictureId": "pictureId", "blobUrl": "blobUrl", "author": "author", "title": "title" }
        // Use PhotoMessage class to serialize the message
        // Create instance of PhotoMessage class and set the properties, use author and title from the query string

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(new { blobUrl, pictureId });
        return response;
    }
}