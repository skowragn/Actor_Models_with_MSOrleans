using CarsManager.Orleans.Application.Cqrs.Commands;
using CarsManager.Orleans.Web.Mapper;
using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;

namespace CarsManager.Orleans.Web.Services;
public class CarsService(IMediator mediator, ILogger<CarsService> logger) : ICarsService
{
    private readonly ILogger _logger = logger;
    private readonly IMediator _mediator = mediator;

    public async Task CreateOrUpdateAvailableCarDetails(CarsDetailsViewModel car)
    {
        var carDetails = car.ToCarDetails();
        await _mediator.Send(new CreateOrUpdateAvailableCarDetailsCommand(carDetails));
    }
}