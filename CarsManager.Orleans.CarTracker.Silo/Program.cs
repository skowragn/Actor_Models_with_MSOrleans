using System.Net;
using CarManager.Orleans.Hubs.Hubs;
using CarsManager.Orleans.CarTracker.Silo.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans((ctx, siloBuilder) => {

    var instanceId = ctx.Configuration.GetValue<int>("InstanceId");
    siloBuilder.UseLocalhostClustering(
        siloPort: 11111 + instanceId,
        gatewayPort: 30000 + instanceId,
        primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, 11111));

    siloBuilder.AddActivityPropagation();
});

builder.WebHost.UseKestrel((ctx, kestrelOptions) =>
{
    var instanceId = ctx.Configuration.GetValue<int>("InstanceId");
    kestrelOptions.ListenLocalhost(5001 + instanceId);
});
builder.Services.AddHostedService<HubListUpdater>();
builder.Services.AddSignalR().AddJsonProtocol();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();
app.UseAuthorization();
app.MapHub<LocationHub>("/locationHub");
app.Run();