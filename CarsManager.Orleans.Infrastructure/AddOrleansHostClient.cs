using CarsManager.Orleans.Infrastructure.Services;
using Orleans.Configuration;

namespace CarsManager.Orleans.Infrastructure;

public static class OrleansHostClientExtenstions
{
    public static IHostBuilder AddOrleansHostClient(this IHostBuilder silohost, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>(EnvironmentVariables.AzureStorageConnectionString);

        silohost.UseOrleansClient(

            (context, siloBuilder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    siloBuilder.UseLocalhostClustering();

                }
                else
                {
                    siloBuilder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "CarReservationCluster";
                        options.ServiceId = nameof(CarReservationService);
                    });

                    siloBuilder.UseAzureStorageClustering(
                        options => options.ConfigureTableServiceClient(connectionString));
                }
            });
            return silohost;
    }
}