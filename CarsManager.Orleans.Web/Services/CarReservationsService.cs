using CarsManager.Orleans.Application.Cqrs.Commands;
using CarsManager.Orleans.Web.Mapper;
using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;

namespace CarsManager.Orleans.Web.Services;
public class CarReservationsService(IMediator mediator, ILogger<CarReservationsService> logger) : ICarReservationsService
{
    private readonly ILogger _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task AddOrUpdateItem(int quantity, CarsDetailsViewModel car)
    {
        var carDetails = car.ToCarDetails();
        await _mediator.Send(new AddOrUpdateItemCommand(quantity, carDetails));
    }

    public async Task CreateOrUpdateCar(CarsDetailsViewModel car)
    {
        var carDetails = car.ToCarDetails();
        await _mediator.Send(new CreateOrUpdateCarCommand(carDetails));
    }

    public async Task<IEnumerable<CarsDetailsViewModel>?> GetAllCarReservations()
    {
        var cars = await _mediator.Send(new GetAllCarReservationsQuery());
        var carsDetailsViewModel = cars.Select(item => item.ToCarDetailsViewModel()).ToList();
        return carsDetailsViewModel;
    }
}