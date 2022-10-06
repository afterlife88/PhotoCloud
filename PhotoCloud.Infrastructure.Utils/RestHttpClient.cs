using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PhotoCloud.Infrastructure.Utils;

public class RestHttpClient : IDisposable
{
#nullable disable
    private readonly HttpClient _client;

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public RestHttpClient(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
        _client = client;
    }


    public async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest model)
    {
        using HttpResponseMessage response =
            await _client.PostAsync(url,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));
        await VerifyResponseAsync(response);
        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result;
    }

    public void Dispose() => _client.Dispose();

    private async Task VerifyResponseAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            ErrorResult error;
            try
            {
                error = await response.Content.ReadFromJsonAsync<ErrorResult>(_jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                error = new ErrorResult(ex.Message);
            }

            throw new ErrorResponseException(error!, response.StatusCode);
        }
    }
}