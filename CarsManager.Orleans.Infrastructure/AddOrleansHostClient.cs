using CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Queries;
using CarsManager.Orleans.Infrastructure.Services;
using Orleans.Configuration;

namespace CarsManager.Orleans.Infrastructure;

public static class OrleansHostClientExtensions
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
                        options.ServiceId = nameof(GetCarsCountQuery);
                    });

                    siloBuilder.UseAzureStorageClustering(
                        options => options.ConfigureTableServiceClient(connectionString));
                }
            });
            return silohost;
    }
}