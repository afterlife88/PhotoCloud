using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader;

public sealed class UploaderFunctions
{
    private readonly RestHttpClient _httpClient;
    private const string MetadataServiceEndpointUrl = "http://localhost:7180/api/saveMetadata";
    private const string LocatorServiceEndpointUrl = " http://localhost:7040/api/extractLocation";

    public UploaderFunctions(IHttpClientFactory httpClientFactory)
    {
        _httpClient = new RestHttpClient(httpClientFactory.CreateClient(), new JsonSerializerOptions());
    }

    [Function("upload")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestData req,
        FunctionContext _)
    {
        var queryDictionary = QueryHelpers.ParseQuery(req.Url.Query);

        var author = queryDictionary["author"];
        var title = queryDictionary["title"];

        // Simulate blob upload
        Thread.Sleep(1000);
        var blobUrl = $"https://example.com/{Guid.NewGuid()}";

        // Simulate call to locator extractor 
        var responseLocator =
            await _httpClient.PostAsync<LocatorRequest, LocatorResponse>(
                LocatorServiceEndpointUrl,
                new LocatorRequest(blobUrl));

        // Simulate call to metadata saver
        var responseMetadataSaver =
            await _httpClient.PostAsync<MetadataRequest, MetadataResponse>(
                MetadataServiceEndpointUrl,
                new MetadataRequest(title, author, new DateTimeOffset(DateTime.UtcNow), responseLocator.Location,
                    blobUrl));

        if (string.IsNullOrEmpty(responseMetadataSaver.Id))
        {
            var badResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await badResponse.WriteStringAsync("Something happened during save");
            return badResponse;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(responseMetadataSaver);
        return response;
    }
    
}