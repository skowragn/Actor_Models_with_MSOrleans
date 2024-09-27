using CarsManager.Orleans.Web.Mapper;
using CarsManager.Orleans.Web.Model;
using CarsManager.Orleans.Web.Services.Interfaces;

namespace CarsManager.Orleans.Web.Services;
public class CarsInventoryService(IMediator mediator, ILogger<CarsInventoryService> logger) : ICarsInventoryService
{
    private readonly ILogger _logger = logger;
    private readonly IMediator _mediator = mediator;
      
    public async Task<IEnumerable<CarsDetailsViewModel>?> GetAllAvailableCarsDetails()
    {
        var cars = await _mediator.Send(new GetAllAvailableCarsDetailsQuery());
        var carsDetailsViewModel = cars.Select(item => item.ToCarDetailsViewModel()).ToList();
        return carsDetailsViewModel;
    }
}