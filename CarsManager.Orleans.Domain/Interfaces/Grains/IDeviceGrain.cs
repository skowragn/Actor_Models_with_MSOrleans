using CarsManager.Orleans.Domain.CarsTracker;
using Orleans;

namespace CarsManager.Orleans.Domain.Interfaces.Grains;

public interface IDeviceGrain : IGrainWithIntegerKey
{
    ValueTask ProcessMessage(DeviceMessage message);
}
