// using Microsoft.Azure.Functions.Worker;
// using Microsoft.Extensions.Logging;
//
// namespace PhotoCloud.MetadataSaver;
//
// public class MetadataSaverSubscriber
// {
//     /// <summary>
//     /// Handler for metadata saver subscription.
//     /// </summary>
//     /// <param name="message"></param>
//     /// <param name="functionContext"></param>
//     [Function("savePhotoMetadata")]
//     public void SaveMetadata(
//         [ServiceBusTrigger("", "", Connection = "ServiceBusConnectionString")]
//         string message,
//         FunctionContext functionContext)
//     {
//         // TODO: Implement me here
//         // Insert the topic name and subscription name into the ServiceBusTrigger attribute of function app above
//         // Read the message deserialize it to PhotoMessage and log the fields to logger
//         // As we not store anything, log the message to console will be enough
//         var logger = functionContext.GetLogger<MetadataSaverSubscriber>();
//         // logger.LogInformation();
//         
//         
//         // Just simulating save operation
//         Thread.Sleep(500);
//     }
//
//     /// <summary>
//     /// Handler for queue
//     /// </summary>
//     /// <param name="message"></param>
//     /// <param name="functionContext"></param>
//     [Function("saveLocation")]
//     public void SaveLocation(
//         [ServiceBusTrigger("", Connection = "ServiceBusConnectionString")]
//         string message,
//         FunctionContext functionContext)
//     {
//         // TODO: Implement me here
//         // Insert the queue name into the ServiceBusTrigger attribute above
//         // Read the message deserialize it to PhotoLocationMessage and log the fields to logger
//         // As we not store anything, log the message to console will be enough
//         var logger = functionContext.GetLogger<MetadataSaverSubscriber>();
//         // logger.LogInformation();
//
//         // Simulate save
//         // NO SQL case - upsert (by having picture ID) json doc for photo extending with object of location by having upsert 
//         // SQL case - insert into separated table with picture id (NO CONSTRAINTS of course) , Longitude,  Latitude
//
//         // Just simulating save operation
//         Thread.Sleep(500);
//     }
// }