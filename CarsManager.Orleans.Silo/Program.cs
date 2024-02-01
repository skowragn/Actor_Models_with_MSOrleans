using CarManager.Orleans.Hubs.Hubs;
using CarsManager.Orleans.Infrastructure;
using CarsManager.Orleans.Silo.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddOrleansHost(builder.Configuration);

/******************************/
builder.WebHost.UseKestrel((ctx, kestrelOptions) =>
{
    //var instanceId = ctx.Configuration.GetValue<int>("InstanceId");
    //kestrelOptions.ListenLocalhost(7151 + instanceId);
});
builder.Services.AddHostedService<HubListUpdater>();
builder.Services.AddSignalR().AddJsonProtocol();
/******************************/

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Grain Silo is active !");

app.MapHub<LocationHub>("/locationHub"); // ASK

app.Run();
