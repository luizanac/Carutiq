Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


//TODO: Create a build to generate application and configure services
var services = new ServiceCollection();

services.AddSingleton<BrokerConfiguration>(BrokerConfiguration.GetConfig());
services.AddScoped<IConnectionHandler, ConnectionHandler>();

var provider = services.BuildServiceProvider();

var server = ActivatorUtilities.CreateInstance<ApplicationServer>(provider);

var tokenSrc = new CancellationTokenSource();
var token = tokenSrc.Token;

var config = provider.GetRequiredService<BrokerConfiguration>();
await server.Listen(() => { Log.Information("Running on {Ip}:{Port}", config.Host, config.Port); }, token);