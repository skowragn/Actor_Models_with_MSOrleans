using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Extensions;
using CarsManager.Orleans.Web.Components;
using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Mapper;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Cars
{
    private HashSet<CarsDetailsViewModel>? _cars;
    private CarModal? _modal;

    [Parameter]
    public string? Id { get; set; }

    [Inject]
    public IDialogService DialogService  { get; set; } = null!;

    [Inject]
    public ICarReservationsService CarReservationsService { get; set; } = null!;

    protected override async Task OnInitializedAsync() => await RefreshCarsDetails();    
    private void CreateNewCar()
    {
        if (_modal is not null)
        {
            var carMock = new CarDetailsDataGenerator().GetCar(CarTypes.Jaguar);
            _modal.Cars = carMock != null ? carMock.ToCarDetailsViewModel() : new CarsDetailsViewModel();
            _modal.Open("Create Car", OnCarUpdated);
        }
    }

    private async Task OnCarUpdated(CarsDetailsViewModel car)
    {
       await CarReservationsService.CreateOrUpdateCar(car);
       await RefreshCarsDetails();


       _modal?.Close();

       StateHasChanged();
    }

    private Task OnEditCar(CarsDetailsViewModel car) =>
        OnCarUpdated(car);

    private async Task RefreshCarsDetails()
    {
        var carsDetails = await CarReservationsService.GetAllCarReservations();

        if (carsDetails != null)
            _cars = new HashSet<CarsDetailsViewModel>(carsDetails);
    }

}