using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Services.Interfaces;

public interface IBookedCarsItemsService
{
    Task<IEnumerable<CarsBookedItemViewModel>?> GetAllBookedCarsItems();
    Task RemoveBookedCarsItem(CarsDetailsViewModel car);
    Task<bool> AddOrUpdateItem(int quantity, CarsDetailsViewModel carDetailsViewModel);
    Task EmptyCarsItem();
    Task<int> GetCarsCount();
}
