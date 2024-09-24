using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Mapper;

public static class CarsBookedItemViewModelMapper
{
    public static CarsBookedItem ToCarsBookedItem(this CarsBookedItemViewModel carBookedItemViewModel)
    {
        CarDetails car = carBookedItemViewModel.Car.ToCarDetails();
        CarsBookedItem carBookedItem = new(carBookedItemViewModel.UserId, carBookedItemViewModel.Quantity, car);
        return carBookedItem;
    }

    public static CarsBookedItemViewModel ToCarsBookedItemViewModel(this CarsBookedItem carBookedItem)
    {
        return new CarsBookedItemViewModel()
        {
            UserId = carBookedItem.UserId,
            Quantity = carBookedItem.Quantity,
            Car = carBookedItem.Car.ToCarDetailsViewModel(),
            TotalPrice = carBookedItem.TotalPrice
        };
    }
}
