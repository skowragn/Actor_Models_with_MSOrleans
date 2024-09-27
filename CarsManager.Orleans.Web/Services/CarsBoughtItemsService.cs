using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;
using CarsManager.Orleans.Web.Mapper;
using CarsManager.Orleans.Application.Cqrs.Commands;

namespace CarsManager.Orleans.Web.Services;

public class CarsBoughtItemsService(IMediator mediator, ILogger<CarsInventoryService> logger) : IBoughtCarsItemsService
{
    private readonly ILogger _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task<bool> AddOrUpdateBoughtCarsItem(int quantity, CarsDetailsViewModel carDetailsViewModel)
    {
        var carDetails = carDetailsViewModel.ToCarDetails();
        var result = await _mediator.Send(new AddOrUpdateBoughtCarsItemCommand(quantity, carDetails));
        return result;
    }

    public async Task<IEnumerable<CarsBoughtItemViewModel>?> GetAllBoughtCarsItems()
    {
       var carsBookedItem = await _mediator.Send(new GetAllBoughtCarsItemsQuery());

        if (carsBookedItem != null && carsBookedItem.Any())
        {
            return carsBookedItem.Select(item => item.ToCarsBookedItemViewModel());
        }
        return [];
    }

    public async Task RemoveBoughtCarsItem(CarsDetailsViewModel car)
    {
        var carDetails = car.ToCarDetails();
        await _mediator.Send(new RemoveBoughtCarsItemCommand(carDetails));
    }

    public async Task EmptyBoughtCarsItem()
    {
        await _mediator.Send(new EmptyBoughtCarsItemCommand());
    }

    public async Task<int> GetCarBoughtCount()
    {
        var result = await _mediator.Send(new GetCarsCountQuery());
        return result;
    }
}