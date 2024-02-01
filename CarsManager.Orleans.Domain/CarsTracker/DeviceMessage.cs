using Orleans.CodeGeneration;

namespace CarsManager.Orleans.Domain.CarsTracker;

[Immutable, GenerateSerializer]
public record class DeviceMessage(
    double Latitude,
    double Longitude,
    long MessageId,
    int DeviceId,
    DateTime Timestamp);
