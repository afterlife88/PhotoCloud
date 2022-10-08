using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader.Result;

// public class UploaderPublisher
// {
//     private readonly IConfiguration _configuration;
//
//     public UploaderPublisher(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }
//
//     [Function("uploadAsync")]
//     public async Task<HttpResponseData> RunUploadAsync(
//         [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
//         HttpRequestData req,
//         FunctionContext _)
//     {
//         var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);
//
//         var author = queryDictionary["author"];
//         var title = queryDictionary["title"];
//
//         // Simulate blob upload
//         Thread.Sleep(1000);
//         var blobUrl = $"https://example.com/{Guid.NewGuid()}";
//         var pictureId = Guid.NewGuid().ToString();
//
//         var serviceBusClient = new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnectionString"));
//         var sender = serviceBusClient.CreateSender("photos");
//         var photoMessage = new PhotoMessage(blobUrl, author, title, pictureId);
//         // var testNewObj 
//         var payload = JsonSerializer.Serialize(photoMessage);
//
//         var message = new ServiceBusMessage(payload);
//         await sender.SendMessageAsync(message);
//
//         await sender.CloseAsync();
//
//         var response = req.CreateResponse(HttpStatusCode.Created);
//         await response.WriteAsJsonAsync(new { blobUrl, pictureId });
//         return response;
//     }
// }