using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Services.Interfaces;

public interface IBoughtCarsItemsService
{
    Task<IEnumerable<CarsBoughtItemViewModel>?> GetAllBoughtCarsItems();
    Task RemoveBoughtCarsItem(CarsDetailsViewModel car);
    Task<bool> AddOrUpdateBoughtCarsItem(int quantity, CarsDetailsViewModel carDetailsViewModel);
    Task EmptyBoughtCarsItem();
    Task<int> GetCarBoughtCount();
}
