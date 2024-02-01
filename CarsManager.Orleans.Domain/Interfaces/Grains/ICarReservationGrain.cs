using Orleans;

namespace CarsManager.Orleans.Domain.Interfaces.Grains;

public interface ICarReservationGrain : IGrainWithStringKey
{
    Task<HashSet<CarDetails>> GetAllCarsAsync();

    Task AddOrUpdateCarAsync(CarDetails productDetails);

    Task RemoveCarAsync(string productId);
}
