using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains;

[Reentrant]
public sealed class CarsBookedItemGrain : Grain, ICarsBookedItemGrain
{
    private readonly IPersistentState<Dictionary<string, CarsBookedItem>> _cart;

    public CarsBookedItemGrain(
        [PersistentState(
            stateName: "bookedCars",
            storageName: "car-reservations")]
        IPersistentState<Dictionary<string, CarsBookedItem>> cart) => _cart = cart;

    async Task<bool> ICarsBookedItemGrain.AddOrUpdateItemAsync(int quantity, CarDetails product)
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

    Task ICarsBookedItemGrain.EmptyCartAsync()
    {
        _cart.State.Clear();
        return _cart.ClearStateAsync();
    }
    Task<HashSet<CarsBookedItem>> ICarsBookedItemGrain.GetAllItemsAsync() =>
        Task.FromResult(_cart.State.Values.ToHashSet());

    Task<int> ICarsBookedItemGrain.GetTotalItemsInCartAsync() =>
        Task.FromResult(_cart.State.Count);

    async Task ICarsBookedItemGrain.RemoveItemAsync(CarDetails product)
    {
        var products = GrainFactory.GetGrain<ICarGrain>(product.Id);
        await products.ReturnCarAsync(product.Quantity);

        if (_cart.State.Remove(product.Id))
        {
            await _cart.WriteStateAsync();
        }
    }

    private CarsBookedItem ToCartItem(int quantity, CarDetails product) =>
        new(this.GetPrimaryKeyString(), quantity, product);
}
