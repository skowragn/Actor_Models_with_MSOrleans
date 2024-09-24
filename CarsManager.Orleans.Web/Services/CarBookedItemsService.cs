using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Mapper;
using CarsManager.Orleans.Application.Cqrs.Commands;

namespace CarsManager.Orleans.Web.Services;

public class CarBookedItemsService(IMediator mediator, ILogger<CarReservationsService> logger) : IBookedCarsItemsService
{
    private readonly ILogger _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task<bool> AddOrUpdateItem(int quantity, CarsDetailsViewModel carDetailsViewModel)
    {
        var carDetails = carDetailsViewModel.ToCarDetails();
        var result = await _mediator.Send(new AddOrUpdateItemCommand(quantity, carDetails));
        return result;
    }

    public async Task<IEnumerable<CarsBookedItemViewModel>?> GetAllBookedCarsItems()
    {
       var carsBookedItem = await _mediator.Send(new GetAllBookedCarsItemsQuery());

        if (carsBookedItem != null && carsBookedItem.Any())
        {
            return carsBookedItem.Select(item => item.ToCarsBookedItemViewModel());
        }
        return [];
    }

    public async Task RemoveBookedCarsItem(CarsDetailsViewModel car)
    {
        var carDetails = car.ToCarDetails();
        await _mediator.Send(new RemoveBookedCarsItemCommand(carDetails));
    }

    public async Task EmptyCarsItem()
    {
        await _mediator.Send(new EmptyCarsItemCommand());
    }

    public async Task<int> GetCarsCount()
    {
        var result = await _mediator.Send(new GetCarsCountQuery());
        return result;
    }
}