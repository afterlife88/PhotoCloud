using HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(
        options => { options.UseMiddleware<ErrorHandlingMiddleware>(); })
    .ConfigureServices(s => { s.AddHttpClient(); })
    .Build();

host.Run();