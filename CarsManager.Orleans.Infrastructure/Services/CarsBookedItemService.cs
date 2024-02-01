using CarsManager.Orleans.Core;
using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using Orleans;

namespace CarsManager.Orleans.Infrastructure.Services;

public sealed class CarsBookedItemService : BaseClusterService
{
    public CarsBookedItemService(
        IHttpContextAccessor httpContextAccessor, IClusterClient client) :
        base(httpContextAccessor, client)
    {
    }

    public Task<HashSet<CarsBookedItem>> GetAllBookedCarsItemsAsync() =>
        TryUseGrain<ICarsBookedItemGrain, Task<HashSet<CarsBookedItem>>>(
            cart => cart.GetAllItemsAsync(),
            () => Task.FromResult(new HashSet<CarsBookedItem>()));

    public Task<int> GetCarsCountAsync() =>
        TryUseGrain<ICarsBookedItemGrain, Task<int>>(
            cart => cart.GetTotalItemsInCartAsync(),
            () => Task.FromResult(0));

    public Task EmptyCarsAsync() =>
        TryUseGrain<ICarsBookedItemGrain, Task>(
            cart => cart.EmptyCartAsync(), 
            () => Task.CompletedTask);

    public Task<bool> AddOrUpdateBookedCarsItemAsync(int quantity, CarDetails car) =>
        TryUseGrain<ICarsBookedItemGrain, Task<bool>>(
            cart => cart.AddOrUpdateItemAsync(quantity, car),
            () => Task.FromResult(false));

    public Task RemoveBookedCarsItemAsync(CarDetails car) =>
        TryUseGrain<ICarsBookedItemGrain, Task>(
            cart => cart.RemoveItemAsync(car),
            () => Task.CompletedTask);
}
