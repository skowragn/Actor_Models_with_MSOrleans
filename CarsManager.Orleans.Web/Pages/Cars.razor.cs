using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Extensions;
using CarsManager.Orleans.Web.Components;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Cars
{
    private HashSet<CarDetails>? _cars;
    private CarModal? _modal;

    [Parameter]
    public string? Id { get; set; }

    [Inject]
    public CarReservationService CarReservationService { get; set; } = null!;

    [Inject]
    public CarService CarService { get; set; } = null!;

    [Inject]
    public IDialogService DialogService  { get; set; } = null!;

    protected override async Task OnInitializedAsync() =>
        _cars = await CarReservationService.GetAllCarReservationsAsync();

    private void CreateNewCar()
    {
        if (_modal is not null)
        {
            var car = new CarDetails();
            var carMock = new CarDetailsDataGenerator().GetCar(CarTypes.Jaguar);
            _modal.Cars = carMock;
            _modal.Open("Create Car", OnCarUpdated);
        }
    }

    private async Task OnCarUpdated(CarDetails car)
    {
        await CarService.CreateOrUpdateCarAsync(car);
        _cars = await CarReservationService.GetAllCarReservationsAsync();

        _modal?.Close();

        StateHasChanged();
    }

    private Task OnEditCar(CarDetails car) =>
        OnCarUpdated(car);
}
