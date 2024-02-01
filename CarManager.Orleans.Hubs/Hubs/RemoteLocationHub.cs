using CarsManager.Orleans.Domain.CarsTracker;
using Microsoft.AspNetCore.SignalR;

namespace CarManager.Orleans.Hubs.Hubs;

/// <summary>
/// Broadcasts location messages to clients which are connected to the local SignalR hub.
/// </summary>
public sealed class RemoteLocationHub : IRemoteLocationHub
{
    private readonly IHubContext<LocationHub> _hub;

    public RemoteLocationHub(IHubContext<LocationHub> hub) => _hub = hub;

    // Send a message to every client which is connected to the hub
    public ValueTask BroadcastUpdates(VelocityBatch messages) =>
        new(_hub.Clients.All.SendAsync(
            "locationUpdates", messages, CancellationToken.None));
}
