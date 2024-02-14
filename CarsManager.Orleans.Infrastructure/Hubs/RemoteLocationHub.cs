using CarsManager.Orleans.Domain.CarsTracker;
using Microsoft.AspNetCore.SignalR;
using CarManager.Orleans.Domain;

namespace CarsManager.Orleans.Infrastructure;

/// <summary>
/// Broadcasts location messages to clients which are connected to the local SignalR hub.
/// </summary>
public sealed class RemoteLocationHub : IRemoteLocationHub
{
    private readonly IHubContext<LocationHub> _hub;

    public RemoteLocationHub(IHubContext<LocationHub> hub) => _hub = hub;

    public ValueTask BroadcastUpdates(VelocityBatch messages) =>
        new(_hub.Clients.All.SendAsync(
            "locationUpdates", messages, CancellationToken.None));
}
