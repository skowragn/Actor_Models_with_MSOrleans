using CarsManager.Orleans.Core.Interfaces.Grains;
using MediatR;
using CarsManager.Orleans.Application.Services;
using CarsManager.Orleans.Application.Cqrs.Commands;

namespace CarsManager.Orleans.IApplication.Extensions.Cqrs.Queries.Handlers;

internal class AddOrUpdateItemCommandHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<AddOrUpdateItemCommand, bool>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task<bool> Handle(AddOrUpdateItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var quantity = request.Quantity;
        var carDetails = request.Car;

        var carBookedItems = _clusterBaseServices.TryUseGrain<ICarsBookedItemGrain, Task<bool>>(
           cart => cart.AddOrUpdateItemAsync(quantity, carDetails),
           () => Task.FromResult(false));
        return carBookedItems;

    }
}