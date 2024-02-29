using System.Net;
using CarsManager.Orleans.Infrastructure.StartupTasks;
using Orleans.Configuration;

namespace CarsManager.Orleans.Infrastructure;

public static class OrleansHostExtenstions
{
    public static IHostBuilder AddOrleansHost(this IHostBuilder silohost, IConfiguration configuration)
    {
        silohost.UseOrleans(

        (context, siloBuilder) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
               var instanceId = context.Configuration.GetValue<int>("InstanceId");
               siloBuilder.UseLocalhostClustering(siloPort: 11111 + instanceId,
                                                  gatewayPort: 30000 + instanceId,
                                                  primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, 11111))
                          .AddMemoryGrainStorage("car-reservations")
                          .AddStartupTask<SeedCarsTask>();
               siloBuilder.AddActivityPropagation();
            }
            else
            {
               var storageConnectionString = configuration.GetValue<string>(EnvironmentVariables.AzureStorageConnectionString);
               var endpointAddress = IPAddress.Parse(context.Configuration["WEBSITE_PRIVATE_IP"] ?? "");
               var strPorts = (context.Configuration["WEBSITE_PRIVATE_PORTS"] ?? "").Split(',');
               if (strPorts.Length < 2)
                    throw new Exception("Insufficient private ports configured.");
               var (siloPort, gatewayPort) = (int.Parse(strPorts[0]), int.Parse(strPorts[1]));
                var connectionString = context.Configuration["ORLEANS_AZURE_STORAGE_CONNECTION_STRING"];

                siloBuilder
                    .ConfigureEndpoints(endpointAddress, siloPort, gatewayPort)
                    .Configure<ClusterOptions>(
                        options =>
                        {
                            options.ClusterId = "CarReservationCluster";
                            options.ServiceId = nameof(OrleansHostExtenstions);
                        }).UseAzureStorageClustering(
                        options => options.ConfigureTableServiceClient(connectionString));
                siloBuilder.AddAzureTableGrainStorage("car-reservations", options => options.ConfigureTableServiceClient(connectionString));
            }
        });
        return silohost;
    }
}