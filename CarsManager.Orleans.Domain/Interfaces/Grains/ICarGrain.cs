using Orleans;

namespace CarsManager.Orleans.Domain.Interfaces.Grains;

public interface ICarGrain : IGrainWithStringKey
{
    Task<(bool IsAvailable, CarDetails? CarDetails)> TryTakeCarAsync(int quantity);

    Task ReturnCarAsync(int quantity);

    Task<int> GetCarAvailabilityAsync();

    Task CreateOrUpdateCarAsync(CarDetails carDetails);

    Task<CarDetails> GetCarDetailsAsync();
}