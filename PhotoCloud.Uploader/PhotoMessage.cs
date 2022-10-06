namespace PhotoCloud.Uploader;

public record PhotoMessage(string BlobUrl, string Author, string Title, string DeviceId, string pictureId);