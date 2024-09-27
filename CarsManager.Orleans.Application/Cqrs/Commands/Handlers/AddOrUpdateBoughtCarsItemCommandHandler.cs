using CarsManager.Orleans.Core.Interfaces.Grains;
using MediatR;
using CarsManager.Orleans.Application.Services;
using CarsManager.Orleans.Application.Cqrs.Commands;

namespace CarsManager.Orleans.IApplication.Extensions.Cqrs.Queries.Handlers;

internal class AddOrUpdateBoughtCarsItemCommandHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<AddOrUpdateBoughtCarsItemCommand, bool>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task<bool> Handle(AddOrUpdateBoughtCarsItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var quantity = request.Quantity;
        var carDetails = request.Car;

        var carBookedItems = _clusterBaseServices.TryUseGrain<ICarsBoughtGrain, Task<bool>>(
           cart => cart.AddOrUpdateItemAsync(quantity, carDetails),
           () => Task.FromResult(false));
        return carBookedItems;

    }
}