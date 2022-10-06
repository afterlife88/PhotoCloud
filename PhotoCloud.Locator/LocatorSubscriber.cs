using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Locator;

public class LocatorSubscriber
{
    [Function("extractLocationMessage")]
    [ServiceBusOutput("photoslocation", Connection = "ServiceBusConnectionString")]
    public string ExtractLocation(
        [ServiceBusTrigger("photos", "Locator", Connection = "ServiceBusConnectionString")]
        string message,
        FunctionContext functionContext)
    {
        var photoMessage = System.Text.Json.JsonSerializer.Deserialize<PhotoMessage>(message)!;
        var logger = functionContext.GetLogger<LocatorSubscriber>();
        logger.LogInformation("Locator msvc - Message received: Picture blob Url: {BlobUrl}", photoMessage!.BlobUrl);

        // Simulate lookup for GPS
        Thread.Sleep(2000);

        var locationMessage = new PhotoLocationMessage(photoMessage.PictureId, new Location(51.5074m, 0.1278m));
        var payload = System.Text.Json.JsonSerializer.Serialize(locationMessage);
        return payload;
    }
}