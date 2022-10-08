using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Locator;

public class LocatorSubscriber
{
    [Function("extractLocationMessage")]
    [ServiceBusOutput("", Connection = "ServiceBusConnectionString")]
    public string ExtractLocation(
        [ServiceBusTrigger("photos", "Locator", Connection = "ServiceBusConnectionString")]
        string message,
        FunctionContext executionContext)
    {
        //TODO: 
        // Populate topic name and subscription name to the ServiceBusTrigger (first attribute topic, second subscription)
        // Read the message and deserialize to PhotoMessage
        // Log the message to the console (LogInformation)
        // Create a new PhotoLocationMessage instance and populate the properties with picture id and location 
        // Example new PhotoLocationMessage(photoMessage.PictureId, new Location(51.5074m, 0.1278m));
        // Serialize the PhotoLocationMessage to string and return it. 
        // Populate ServiceBusOutput with queue name
        var logger = executionContext.GetLogger<LocatorSubscriber>();
        // logger.LogInformation("Locator msvc - Message received: Picture blob Url: {BlobUrl}",
        //     photoMessage!.BlobUrl);

        // Simulate lookup for GPS
        Thread.Sleep(2000);

        // Replace me with serialized PhotoLocationMessage message
        return string.Empty;
    }
}