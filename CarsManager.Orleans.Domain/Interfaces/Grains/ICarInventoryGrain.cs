using Orleans;

namespace CarsManager.Orleans.Domain.Interfaces.Grains;

public interface ICarInventoryGrain : IGrainWithStringKey
{
    Task<HashSet<CarDetails>> GetAllCarsAsync();

    Task AddOrUpdateCarAsync(CarDetails carsDetails);

    Task RemoveCarAsync(string carId);
}
