using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Infrastructure.Services;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Reservations
{
    private HashSet<CarDetails>? _cars;
    private HashSet<CarsBookedItem>? _cartItems;

    [Inject]
    public CarsBookedItemService CarBookedItemsService { get; set; } = null!;

    [Inject]
    public CarReservationService InventoryService { get; set; } = null!;

    [Inject]
    public ComponentStateChangedObserver Observer { get; set; } = null!;

    [Inject]
    public ToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _cars = await InventoryService.GetAllCarReservationsAsync();

        _cartItems = await CarBookedItemsService.GetAllBookedCarsItemsAsync();
    }

    private async Task OnAddedToCart(string productId)
    {
        var car = _cars?.FirstOrDefault(p => p.Id == productId);
        if (car is null)
        {
            return;
        }

        if (await CarBookedItemsService.AddOrUpdateBookedCarsItemAsync(1, car))
        {
            _cars = await InventoryService.GetAllCarReservationsAsync();
            _cartItems = await CarBookedItemsService.GetAllBookedCarsItemsAsync();

            await ToastService.ShowToastAsync(
                "Added to reservation",
                $"The '{car.Name}' for {car.Price:C2} was added to your reservation...");
            await Observer.NotifyStateChangedAsync();

            StateHasChanged();
        }
    }

    private bool IsCarAlreadyInCart(CarDetails product) =>
        _cartItems?.Any(c => c.Car.Id == product.Id) ?? false;
}
