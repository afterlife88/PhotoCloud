namespace PhotoCloud.Locator.Result;

// public class LocatorSubscriber
// {
//     [Function("extractLocationMessage")]
//     [ServiceBusOutput("photoslocation", Connection = "ServiceBusConnectionString")]
//     public string ExtractLocation(
//         [ServiceBusTrigger("photos", "Locator", Connection = "ServiceBusConnectionString")]
//         string message,
//         FunctionContext executionContext)
//     {
//         var photoMessage =
//             System.Text.Json.JsonSerializer.Deserialize<PhotoMessage>(message);
//         var logger = executionContext.GetLogger<LocatorSubscriber>();
//         logger.LogInformation("Locator msvc - Message received: Picture blob Url: {BlobUrl}",
//             photoMessage!.BlobUrl);
//
//         // Simulate lookup for GPS
//         Thread.Sleep(2000);
//
//         var locationMessage = new PhotoLocationMessage(photoMessage.PictureId, new Location(51.5074m, 0.1278m));
//         var payload = System.Text.Json.JsonSerializer.Serialize(locationMessage);
//         return payload;
//     }
// }