using CarsManager.Orleans.Domain.CarsTracker;

namespace CarManager.Orleans.Domain;

public interface IRemoteLocationHub : IGrainObserver
{
    ValueTask BroadcastUpdates(VelocityBatch messages);
}
