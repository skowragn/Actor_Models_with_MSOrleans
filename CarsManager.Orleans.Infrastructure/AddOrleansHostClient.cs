﻿using Orleans.Configuration;

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
                        options.ClusterId = Defaults.ClusterId;
                        options.ServiceId = nameof(OrleansHostExtenstions);
                    });

                    siloBuilder.UseAzureStorageClustering(
                        options => options.ConfigureTableServiceClient(connectionString));
                }
            });
            return silohost;
    }
}