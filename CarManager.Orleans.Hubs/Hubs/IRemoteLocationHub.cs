using CarsManager.Orleans.Domain.CarsTracker;

namespace CarManager.Orleans.Hubs.Hubs;

public interface IRemoteLocationHub : IGrainObserver
{
    ValueTask BroadcastUpdates(VelocityBatch messages);
}
