using CarsManager.Orleans.Grains.CarTracker;
using CarManager.Orleans.Domain;
using Microsoft.AspNetCore.SignalR;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace CarsManager.Orleans.Infrastructure;

[Reentrant]
public sealed class HubListUpdater : BackgroundService
{
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<HubListUpdater> _logger;
    private readonly ILocalSiloDetails _localSiloDetails;
    private readonly RemoteLocationHub _locationBroadcaster;

    public HubListUpdater(
        IGrainFactory grainFactory,
        ILogger<HubListUpdater> logger,
        ILocalSiloDetails localSiloDetails,
        IHubContext<LocationHub> hubContext)
    {
        _grainFactory = grainFactory;
        _logger = logger;
        _localSiloDetails = localSiloDetails;
        _locationBroadcaster = new RemoteLocationHub(hubContext);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var hubListGrain = _grainFactory.GetGrain<IHubListGrain>(Guid.Empty);
        var localSiloAddress = _localSiloDetails.SiloAddress;
        IRemoteLocationHub selfReference = _grainFactory.CreateObjectReference<IRemoteLocationHub>(_locationBroadcaster) as IRemoteLocationHub;

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await hubListGrain.AddHub(localSiloAddress, selfReference);
            }
            catch (Exception exception) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError(exception, "Error polling location hub list");
            }

            if (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (Exception exception) 
                {
                    _logger.LogError(exception, "Error polling location hub list");
                }
            }
        }
    }
}
