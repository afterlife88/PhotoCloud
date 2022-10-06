using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace PhotoCloud.Infrastructure.Utils;

public class ErrorHandlingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("ErrorHandling");
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing invocation");

            var httpReqData = await context.GetHttpRequestDataAsync();

            if (httpReqData != null)
            {
                var newResponse = httpReqData.CreateResponse();
                await newResponse.WriteAsJsonAsync(new { error = ex.Message });

                context.GetInvocationResult().Value = newResponse;
            }
        }
    }
}