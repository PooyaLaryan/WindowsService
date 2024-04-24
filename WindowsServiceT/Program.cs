using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using WindowsServiceT;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MyParameter>(builder.Configuration.GetSection("MyParameter"));
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "PouyaService";
});


LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);


builder.Services.AddSingleton<JokeService>();
builder.Services.AddHostedService<WindowsBackgroundService>();

var host = builder.Build();
host.Run();
