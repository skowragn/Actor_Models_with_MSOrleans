using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Mapper;

public static class CarsBookedItemViewModelMapper
{
    public static CarsBoughtItem ToCarsBookedItem(this CarsBoughtItemViewModel carBookedItemViewModel)
    {
        CarDetails car = carBookedItemViewModel.Car.ToCarDetails();
        CarsBoughtItem carBookedItem = new(carBookedItemViewModel.UserId, carBookedItemViewModel.Quantity, car);
        return carBookedItem;
    }

    public static CarsBoughtItemViewModel ToCarsBookedItemViewModel(this CarsBoughtItem carBookedItem)
    {
        return new CarsBoughtItemViewModel()
        {
            UserId = carBookedItem.UserId,
            Quantity = carBookedItem.Quantity,
            Car = carBookedItem.Car.ToCarDetailsViewModel(),
            TotalPrice = carBookedItem.TotalPrice
        };
    }
}
