using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoCloud.Infrastructure.Utils.ErrorHandling;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(
        options => { options.UseMiddleware<ErrorHandlingMiddleware>(); })
    .ConfigureServices(s => { s.AddHttpClient(); })
    .Build();

host.Run();