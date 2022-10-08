namespace PhotoCloud.MetadataSaver.Result;

// public class MetadataSaverSubscriber
// {
//     [Function("savePhotoMetadata")]
//     public void SaveMetadata(
//         [ServiceBusTrigger("photos", "MetadataSaver", Connection = "ServiceBusConnectionString")]
//         string message,
//         FunctionContext functionContext)
//     {
//         var photoMessage = System.Text.Json.JsonSerializer.Deserialize<PhotoMessage>(message)!;
//         var logger = functionContext.GetLogger<MetadataSaverSubscriber>();
//         logger.LogInformation(
//             "Metadata saver - Message received: Picture blob Url: {BlobUrl}, picID: {PictureId}, author {Author}, title: {Title}",
//             photoMessage!.BlobUrl,
//             photoMessage.PictureId, photoMessage.Author, photoMessage.Title);
//     
//         // Simulate save
//         Thread.Sleep(500);
//     }
//
//     [Function("saveLocation")]
//     public void SaveLocation(
//         [ServiceBusTrigger("photoslocation", Connection = "ServiceBusConnectionString")]
//         string message,
//         FunctionContext functionContext)
//     {
//         var photoLocationMessage = System.Text.Json.JsonSerializer.Deserialize<PhotoLocationMessage>(message)!;
//         var logger = functionContext.GetLogger<MetadataSaverSubscriber>();
//         logger.LogInformation(
//             "Metadata saver - Location Message received: Picture id: {Id}, Longitude: {Longitude}, Latitude {Latitude}",
//             photoLocationMessage.PictureId, photoLocationMessage.Location.Latitude,
//             photoLocationMessage.Location.Longitude);
//     
//         // NO SQL case - upsert (by having picture ID) json doc for photo extending with object of location by having upsert 
//         // SQL case - insert into separated table with picture id (NO CONSTRAINTS of course) , Longitude,  Latitude
//     
//         // Simulate save
//         Thread.Sleep(500);
//     }
// }