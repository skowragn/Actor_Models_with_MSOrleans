using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Extensions;
using CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Commands;
using CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;
using CarsManager.Orleans.Web.Components;

namespace CarsManager.Orleans.Web.Pages;

public sealed partial class Cars
{
    private HashSet<CarDetails>? _cars;
    private CarModal? _modal;

    [Parameter]
    public string? Id { get; set; }

    [Inject]
    public IDialogService DialogService  { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    protected override async Task OnInitializedAsync() => _cars = await Mediator.Send(new GetAllCarReservationsQuery());
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
        await Mediator.Send(new CreateOrUpdateCarCommand(car));
        _cars = await Mediator.Send(new GetAllCarReservationsQuery());

        _modal?.Close();

        StateHasChanged();
    }

    private Task OnEditCar(CarDetails car) =>
        OnCarUpdated(car);
}