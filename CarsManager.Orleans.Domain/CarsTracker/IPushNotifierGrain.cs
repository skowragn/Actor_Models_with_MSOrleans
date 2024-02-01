using Orleans;

namespace CarsManager.Orleans.Domain.CarsTracker;

public interface IPushNotifierGrain : IGrainWithIntegerKey
{
    ValueTask SendMessage(VelocityMessage message);
}
