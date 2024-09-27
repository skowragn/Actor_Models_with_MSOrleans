using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;

namespace CarsManager.Orleans.Web.Pages;

public partial class BoughtCarCart
    {
    private HashSet<CarsBoughtItemViewModel>? _carsItems;

     [Inject]
       public IBoughtCarsItemsService CarBoughtItemsService { get; set; } = null!;

    [Inject]
        public ComponentStateChangedObserver Observer { get; set; } = null!;

        protected override Task OnInitializedAsync() => GetCartItemsAsync();

    private Task GetCartItemsAsync()
    {
        return InvokeAsync(async () =>
        {
            var bookedCarsItems = await CarBoughtItemsService.GetAllBoughtCarsItems();

            if (bookedCarsItems!=null && bookedCarsItems.Any())
            _carsItems = new HashSet<CarsBoughtItemViewModel>(bookedCarsItems);

            StateHasChanged();
        });
    }

    private async Task OnItemRemovedAsync(CarsDetailsViewModel car)
    {

        await CarBoughtItemsService.RemoveBoughtCarsItem(car);
        await Observer.NotifyStateChangedAsync();

        _ = _carsItems?.RemoveWhere(item => item.Car.Id == car.Id);
    }

        private async Task OnItemUpdatedAsync((int Quantity, CarsDetailsViewModel Product) tuple)
        {
           await CarBoughtItemsService.AddOrUpdateBoughtCarsItem(tuple.Quantity, tuple.Product);
           await GetCartItemsAsync();
        }

        private async Task EmptyCartAsync()
        {
           await CarBoughtItemsService.EmptyBoughtCarsItem();
           await Observer.NotifyStateChangedAsync();

            _carsItems?.Clear();
        }
    }