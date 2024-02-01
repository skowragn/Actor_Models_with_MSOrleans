using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans;

namespace CarsManager.Orleans.Infrastructure.Services;

public sealed class CarService : BaseClusterService
{
    public CarService(
        IHttpContextAccessor httpContextAccessor, IClusterClient client) :
        base(httpContextAccessor, client)
    {
    }

    public Task CreateOrUpdateCarAsync(CarDetails car) =>
        _client.GetGrain<ICarGrain>(car.Id).CreateOrUpdateCarAsync(car);

    public Task<(bool IsAvailable, CarDetails? CarDetails)> TryTakeCarAsync(
        string productId, int quantity) =>
        TryUseGrain<ICarGrain, Task<(bool IsAvailable, CarDetails? CarDetails)>>(
            products => products.TryTakeCarAsync(quantity),
            productId,
            () => Task.FromResult<(bool IsAvailable, CarDetails? CarDetails)>(
                (false, null)));

    public Task ReturnCarAsync(string carId, int quantity) =>
        TryUseGrain<ICarGrain, Task>(
            cars => cars.ReturnCarAsync(quantity),
            carId,
            () => Task.CompletedTask);

    public Task<int> GetCarAvailability(string carId) =>
        TryUseGrain<ICarGrain, Task<int>>(
            cars => cars.GetCarAvailabilityAsync(),
            carId,
            () => Task.FromResult(0));
}
