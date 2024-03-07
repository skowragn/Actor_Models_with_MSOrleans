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
               var instanceId = context.Configuration.GetValue<int>(Defaults.InstanceId);
               siloBuilder.UseLocalhostClustering(siloPort: Defaults.SiloPort + instanceId,
                                                  gatewayPort: Defaults.GatewayPort + instanceId,
                                                  primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, Defaults.SiloPort))
                          .AddMemoryGrainStorage(Defaults.CarReservations)
                          .AddStartupTask<SeedCarsTask>();
               siloBuilder.AddActivityPropagation();
            }
            else
            {
               var storageConnectionString = configuration.GetValue<string>(EnvironmentVariables.AzureStorageConnectionString);
               var endpointAddress = IPAddress.Parse(context.Configuration[EnvironmentVariables.WebAppsPrivatePorts] ?? "");
               var strPorts = (context.Configuration[EnvironmentVariables.WebAppsPrivatePorts] ?? "").Split(',');
               if (strPorts.Length < 2)
                    throw new Exception("Insufficient private ports configured.");
               var (siloPort, gatewayPort) = (int.Parse(strPorts[0]), int.Parse(strPorts[1]));
                var connectionString = context.Configuration[EnvironmentVariables.AzureStorageConnectionString];

                siloBuilder
                    .ConfigureEndpoints(endpointAddress, siloPort, gatewayPort)
                    .Configure<ClusterOptions>(
                        options =>
                        {
                            options.ClusterId = Defaults.ClusterId;
                            options.ServiceId = nameof(OrleansHostExtenstions);
                        }).UseAzureStorageClustering(
                        options => options.ConfigureTableServiceClient(connectionString));
                siloBuilder.AddAzureTableGrainStorage(Defaults.CarReservations, options => options.ConfigureTableServiceClient(connectionString));
            }
        });
        return silohost;
    }
}