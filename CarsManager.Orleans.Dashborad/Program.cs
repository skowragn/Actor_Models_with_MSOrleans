var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Host.UseOrleans(

    (context, siloBuilder) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
        {
            siloBuilder.UseLocalhostClustering()
                       .AddMemoryGrainStorage("car-reservations")
                       .UseDashboard(dashboardOptions => dashboardOptions.HostSelf = false);
        }
        else
        {
            var storageConnectionString = builder.Configuration.GetValue<string>(EnvironmentVariables.AzureStorageConnectionString);

            siloBuilder.UseLocalhostClustering()
                       .AddAzureTableGrainStorage(name: "car-reservations",options => options.ConfigureTableServiceClient(storageConnectionString))
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