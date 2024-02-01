using Orleans;
using Orleans.Hosting;
using CarsManager.Orleans.Grains;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Host.UseOrleans(

    (context, siloBuilder) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
        {
            siloBuilder.UseLocalhostClustering()
                       .AddMemoryGrainStorage("car-reservations")
                       .ConfigureApplicationParts(applicationParts => applicationParts.AddApplicationPart(typeof(CarGrain).Assembly).WithReferences())
                       .UseDashboard(dashboardOptions => dashboardOptions.HostSelf = false);
        }
        else
        {
            var storageConnectionString = builder.Configuration.GetValue<string>(EnvironmentVariables.AzureStorageConnectionString);
            siloBuilder.HostSiloInAzure(builder.Configuration)
                       .AddAzureTableGrainStorage(name: "car-reservations", options => options.ConfigureTableServiceClient(storageConnectionString))
                       .ConfigureApplicationParts(applicationParts => applicationParts.AddApplicationPart(typeof(CarGrain).Assembly).WithReferences())
                       .UseDashboard(dashboardOptions => dashboardOptions.HostSelf = false);
        }
    });

builder.Services.AddServicesForSelfHostedDashboard();

var app = builder.Build();
app.UseOrleansDashboard();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();