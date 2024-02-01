using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Infrastructure.Services;

namespace CarsManager.Orleans.Web.Pages;

    public partial class BookedCarCart
    {
        private HashSet<CarsBookedItem>? _carsItems;

        [Inject]
        public CarsBookedItemService CarsBookedItem { get; set; } = null!;

        [Inject]
        public ComponentStateChangedObserver Observer { get; set; } = null!;

        protected override Task OnInitializedAsync() => GetCartItemsAsync();

        private Task GetCartItemsAsync() =>
            InvokeAsync(async () =>
            {
                _carsItems = await CarsBookedItem.GetAllBookedCarsItemsAsync();
                StateHasChanged();
            });

        private async Task OnItemRemovedAsync(CarDetails car)
        {
            await CarsBookedItem.RemoveBookedCarsItemAsync(car);
            await Observer.NotifyStateChangedAsync();

            _ = _carsItems?.RemoveWhere(item => item.Car == car);
        }

        private async Task OnItemUpdatedAsync((int Quantity, CarDetails Product) tuple)
        {
            await CarsBookedItem.AddOrUpdateBookedCarsItemAsync(tuple.Quantity, tuple.Product);
            await GetCartItemsAsync();
        }

        private async Task EmptyCartAsync()
        {
            await CarsBookedItem.EmptyCarsAsync();
            await Observer.NotifyStateChangedAsync();

            _carsItems?.Clear();
        }
    }