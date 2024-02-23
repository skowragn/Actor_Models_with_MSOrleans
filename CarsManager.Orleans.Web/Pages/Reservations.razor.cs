using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Reservations
{
    private HashSet<CarDetails>? _cars;
    private HashSet<CarsBookedItem>? _cartItems;

    [Inject]
    public ComponentStateChangedObserver Observer { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    [Inject]
    public ToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _cars = await Mediator.Send(new GetAllCarReservationsQuery());
        _cartItems = await Mediator.Send(new GetAllBookedCarsItemsQuery());
    }

    private async Task OnAddedToCart(string productId)
    {
        var car = _cars?.FirstOrDefault(p => p.Id == productId);
        if (car is null)
        {
            return;
        }

        if (await Mediator.Send(new AddOrUpdateItemCommand(1, car)))
        {
            _cars = await Mediator.Send(new GetAllCarReservationsQuery());
            _cartItems = await Mediator.Send(new GetAllBookedCarsItemsQuery());

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
