using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Pages;

    public partial class BookedCarCart
    {
    private HashSet<CarsBookedItemViewModel>? _carsItems;

     [Inject]
       public IBookedCarsItemsService BookedCarsItemsService { get; set; } = null!;

    [Inject]
        public ComponentStateChangedObserver Observer { get; set; } = null!;

        protected override Task OnInitializedAsync() => GetCartItemsAsync();

    private Task GetCartItemsAsync()
    {
        return InvokeAsync(async () =>
        {
            var bookedCarsItems = await BookedCarsItemsService.GetAllBookedCarsItems();

            if(bookedCarsItems!=null && bookedCarsItems.Any())
            _carsItems = new HashSet<CarsBookedItemViewModel>(bookedCarsItems);

            StateHasChanged();
        });
    }

    private async Task OnItemRemovedAsync(CarsDetailsViewModel car)
    {

        await BookedCarsItemsService.RemoveBookedCarsItem(car);
        await Observer.NotifyStateChangedAsync();

        _ = _carsItems?.RemoveWhere(item => item.Car.Id == car.Id);
    }

        private async Task OnItemUpdatedAsync((int Quantity, CarsDetailsViewModel Product) tuple)
        {
           await BookedCarsItemsService.AddOrUpdateItem(tuple.Quantity, tuple.Product);
           await GetCartItemsAsync();
        }

        private async Task EmptyCartAsync()
        {
           await BookedCarsItemsService.EmptyCarsItem();
           await Observer.NotifyStateChangedAsync();

            _carsItems?.Clear();
        }
    }