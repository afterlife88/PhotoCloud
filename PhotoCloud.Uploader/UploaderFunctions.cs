using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using PhotoCloud.Infrastructure.Utils;

namespace PhotoCloud.Uploader;

public sealed class UploaderFunctions
{
    private readonly RestHttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public UploaderFunctions(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _configuration = configuration;
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
                _configuration["LocatorServiceEndpointUrl"],
                new LocatorRequest(blobUrl));

        // Simulate call to metadata saver
        var responseMetadataSaver =
            await _httpClient.PostAsync<MetadataRequest, MetadataResponse>(
                _configuration["MetadataServiceEndpointUrl"],
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