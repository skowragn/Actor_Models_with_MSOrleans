using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class CarsInventory
 {
    private HashSet<CarsDetailsViewModel>? _cars;
    private HashSet<CarsBoughtItemViewModel>? _cartItems;

    [Inject]
    public ComponentStateChangedObserver Observer { get; set; } = null!;

    [Inject]
    public ICarsInventoryService CarsInventoryService { get; set; } = null!;

    [Inject]
    public IBoughtCarsItemsService BoughtCarsItemsService { get; set; } = null!;

    [Inject]
    public ToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshCarsDetailsAndBookedCars();
    }

    private async Task OnAddedToCart(string productId)
    {
        var car = _cars?.FirstOrDefault(p => p.Id == productId);
        if (car is null)
        {
            return;
        }

        if (await BoughtCarsItemsService.AddOrUpdateBoughtCarsItem(1, car))
        {
            await RefreshCarsDetailsAndBookedCars();

            await ToastService.ShowToastAsync(
                "Added to reservation",
                $"The '{car.Name}' for {car.Price:C2} was added to your reservation...");
            await Observer.NotifyStateChangedAsync();

            StateHasChanged();
        }
    }

    private async Task RefreshCarsDetailsAndBookedCars()
    {
        var carsDetails = await CarsInventoryService.GetAllAvailableCarsDetails();
        var bookedCarsItems = await BoughtCarsItemsService.GetAllBoughtCarsItems();

        if (carsDetails != null)
            _cars = new HashSet<CarsDetailsViewModel>(carsDetails);

        if (bookedCarsItems != null)
            _cartItems = new HashSet<CarsBoughtItemViewModel>(bookedCarsItems);
    }

    private bool IsCarAlreadyInCart(CarsDetailsViewModel car) =>
        _cartItems?.Any(c => c.Car.Id == car.Id) ?? false;
}
