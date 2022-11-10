using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    // Commented out for deadlettering
    // options => { options.UseMiddleware<ErrorHandlingMiddleware>(); })
    .ConfigureServices(s => { s.AddHttpClient(); })
    .Build();

host.Run();