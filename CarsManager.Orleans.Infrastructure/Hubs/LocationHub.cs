using Microsoft.AspNetCore.SignalR;

namespace CarsManager.Orleans.Infrastructure;

/// <summary>
/// The hub which Web clients connect to to receive location updates. Messages are broadcast by <see cref="RemoteLocationHub"/> using <see cref="IHubContext{LocationHub}"/>.
/// </summary>
public sealed class LocationHub : Hub;