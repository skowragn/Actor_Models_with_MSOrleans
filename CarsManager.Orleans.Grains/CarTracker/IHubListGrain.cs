using CarManager.Orleans.Domain;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains.CarTracker;

public interface IHubListGrain : IGrainWithGuidKey
{
    ValueTask AddHub(SiloAddress host, IRemoteLocationHub hubReference);
    ValueTask<List<(SiloAddress Host, IRemoteLocationHub Hub)>> GetHubs();
}
