using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Domain;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Reservations
{
    private HashSet<CarsDetailsViewModel>? _cars;
    private HashSet<CarsBookedItemViewModel>? _cartItems;

    [Inject]
    public ComponentStateChangedObserver Observer { get; set; } = null!;

    [Inject]
    public ICarReservationsService CarReservationsService { get; set; } = null!;

    [Inject]
    public IBookedCarsItemsService BookedCarsItemsService { get; set; } = null!;

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

        if (await BookedCarsItemsService.AddOrUpdateItem(1, car))
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
        var carsDetails = await CarReservationsService.GetAllCarReservations();
        var bookedCarsItems = await BookedCarsItemsService.GetAllBookedCarsItems();

        if (carsDetails != null)
            _cars = new HashSet<CarsDetailsViewModel>(carsDetails);

        if (bookedCarsItems != null)
            _cartItems = new HashSet<CarsBookedItemViewModel>(bookedCarsItems);
    }

    private bool IsCarAlreadyInCart(CarsDetailsViewModel product) =>
        _cartItems?.Any(c => c.Car.Id == product.Id) ?? false;
}
