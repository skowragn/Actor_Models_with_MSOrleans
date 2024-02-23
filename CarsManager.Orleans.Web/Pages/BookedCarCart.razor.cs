using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;

namespace CarsManager.Orleans.Web.Pages;

    public partial class BookedCarCart
    {
        private HashSet<CarsBookedItem>? _carsItems;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    [Inject]
        public ComponentStateChangedObserver Observer { get; set; } = null!;

        protected override Task OnInitializedAsync() => GetCartItemsAsync();

        private Task GetCartItemsAsync() =>
            InvokeAsync(async () =>
            {
                _carsItems = await Mediator.Send(new GetAllBookedCarsItemsQuery());
                StateHasChanged();
            });

        private async Task OnItemRemovedAsync(CarDetails car)
        {
            await Mediator.Send(new RemoveBookedCarsItemCommand(car));
            await Observer.NotifyStateChangedAsync();

            _ = _carsItems?.RemoveWhere(item => item.Car == car);
        }

        private async Task OnItemUpdatedAsync((int Quantity, CarDetails Product) tuple)
        {
           await Mediator.Send(new AddOrUpdateItemCommand(tuple.Quantity, tuple.Product));
           await GetCartItemsAsync();
        }

        private async Task EmptyCartAsync()
        {
           await Mediator.Send(new EmptyCarsItemCommand());
           await Observer.NotifyStateChangedAsync();

            _carsItems?.Clear();
        }
    }