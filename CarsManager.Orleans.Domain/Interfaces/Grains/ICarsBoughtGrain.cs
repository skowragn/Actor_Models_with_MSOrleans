using CarsManager.Orleans.Domain;
using Orleans;

namespace CarsManager.Orleans.Core.Interfaces.Grains;

public interface ICarsBoughtGrain : IGrainWithStringKey
{
    /// <summary>
    /// Adds the given <paramref name="quantity"/> of the corresponding
    /// <paramref name="product"/> to the shopping cart.
    /// </summary>
    Task<bool> AddOrUpdateItemAsync(int quantity, CarDetails product);

    /// <summary>
    /// Removes the given <paramref name="product" /> from the shopping cart.
    /// </summary>
    Task RemoveItemAsync(CarDetails product);
    
    /// <summary>
    /// Gets all the items in the shopping cart.
    /// </summary>
    Task<HashSet<CarsBoughtItem>> GetAllItemsAsync();

    /// <summary>
    /// Gets the number of items in the shopping cart.
    /// </summary>
    Task<int> GetTotalItemsInCartAsync();

    /// <summary>
    /// Removes all items from the shopping cart.
    /// </summary>
    Task EmptyCartAsync();
}