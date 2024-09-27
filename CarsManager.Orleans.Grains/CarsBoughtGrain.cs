using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains;

[Reentrant]
public sealed class CarsBoughtGrain : Grain, ICarsBoughtGrain
{
    private readonly IPersistentState<Dictionary<string, CarsBoughtItem>> _cart;

    public CarsBoughtGrain(
        [PersistentState(
            stateName: "BoughtCars",
            storageName: "car-reservations")]
        IPersistentState<Dictionary<string, CarsBoughtItem>> cart) => _cart = cart;

    async Task<bool> ICarsBoughtGrain.AddOrUpdateItemAsync(int quantity, CarDetails product)
    {
        var products = GrainFactory.GetGrain<ICarGrain>(product.Id);
   
        int? adjustedQuantity = null;
        if (_cart.State.TryGetValue(product.Id, out var existingItem))
        {
            adjustedQuantity = quantity - existingItem.Quantity;
        }

        var (isAvailable, claimedProduct) =
            await products.TryTakeCarAsync(adjustedQuantity ?? quantity);
        if (isAvailable && claimedProduct is not null)
        {
            var item = ToCartItem(quantity, claimedProduct);
            _cart.State[claimedProduct.Id] = item;

            await _cart.WriteStateAsync();
            return true;
        }

        return false;
    }

    Task ICarsBoughtGrain.EmptyCartAsync()
    {
        _cart.State.Clear();
        return _cart.ClearStateAsync();
    }
    Task<HashSet<CarsBoughtItem>> ICarsBoughtGrain.GetAllItemsAsync() =>
        Task.FromResult(_cart.State.Values.ToHashSet());

    Task<int> ICarsBoughtGrain.GetTotalItemsInCartAsync() =>
        Task.FromResult(_cart.State.Count);

    async Task ICarsBoughtGrain.RemoveItemAsync(CarDetails product)
    {
        var products = GrainFactory.GetGrain<ICarGrain>(product.Id);
        await products.ReturnCarAsync(product.Quantity);

        if (_cart.State.Remove(product.Id))
        {
            await _cart.WriteStateAsync();
        }
    }

    private CarsBoughtItem ToCartItem(int quantity, CarDetails product) =>
        new(this.GetPrimaryKeyString(), quantity, product);
}
