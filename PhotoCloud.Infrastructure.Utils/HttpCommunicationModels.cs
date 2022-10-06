namespace PhotoCloud.Infrastructure.Utils;

public record LocatorRequest(string BlobUrl);

public record LocatorResponse(Location Location);

public record MetadataRequest(string Title, string Author, DateTimeOffset Date, Location Location, string BlobUrl);

public record MetadataResponse(string Title, string Author, DateTimeOffset Date, Location Location, string Id);

public record Location(decimal Longitude, decimal Latitude);