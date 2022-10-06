namespace PhotoCloud.Infrastructure.Utils.Messages;

public record PhotoMessage(string BlobUrl, string Author, string Title, string DeviceId, string PictureId);

public record PhotoLocationMessage(string PictureId, Location Location);