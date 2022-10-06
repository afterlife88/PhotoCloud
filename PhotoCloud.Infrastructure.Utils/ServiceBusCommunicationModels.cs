namespace PhotoCloud.Infrastructure.Utils;

public record PhotoMessage(string BlobUrl, string Author, string Title, string PictureId);

public record PhotoLocationMessage(string PictureId, Location Location);