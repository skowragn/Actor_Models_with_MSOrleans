using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Infrastructure.Services;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries.Handlers;

internal class RemoveBookedCarsItemCommandHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<RemoveBookedCarsItemCommand>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task Handle(RemoveBookedCarsItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

       _clusterBaseServices.TryUseGrain<ICarsBookedItemGrain, Task>(
            cart => cart.RemoveItemAsync(request.Car),
            () => Task.CompletedTask);

        return Task.CompletedTask;
    }
}