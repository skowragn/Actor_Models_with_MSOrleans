using CarManager.Orleans.Domain;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains.CarTracker;

public class HubListGrain : Grain, IHubListGrain
{
    private readonly IClusterMembershipService _clusterMembership;
    private readonly Dictionary<SiloAddress, IRemoteLocationHub> _hubs = new();
    private MembershipVersion _cacheMembershipVersion;
    private List<(SiloAddress Host, IRemoteLocationHub Hub)>? _cache;

    public HubListGrain(IClusterMembershipService clusterMembershipService)
    {
        _clusterMembership = clusterMembershipService;
    }

    public ValueTask AddHub(SiloAddress host, IRemoteLocationHub hubReference)
    {
        _cache = null;
        _hubs[host] = hubReference;

        return default;
    }

    public ValueTask<List<(SiloAddress Host, IRemoteLocationHub Hub)>> GetHubs() =>
        new(GetCachedHubs());

    private List<(SiloAddress Host, IRemoteLocationHub Hub)> GetCachedHubs()
    {
        var clusterMembers = _clusterMembership.CurrentSnapshot;
        if (_cache is not null && clusterMembers.Version == _cacheMembershipVersion)
        {
            return _cache;
        }
        var hubs = new List<(SiloAddress Host, IRemoteLocationHub Hub)>();
        var toDelete = new List<SiloAddress>();
        foreach (var (host, hubRef) in _hubs)
        {
            SiloStatus hostStatus = clusterMembers.GetSiloStatus(host);
            if (hostStatus is SiloStatus.Dead)
            {
                toDelete.Add(host);
            }

            if (hostStatus is SiloStatus.Active)
            {
                hubs.Add((host, hubRef));
            }
        }

        foreach (SiloAddress host in toDelete)
        {
            _hubs.Remove(host);
        }

        _cache = hubs;
        _cacheMembershipVersion = clusterMembers.Version;
        return hubs;
    }
}