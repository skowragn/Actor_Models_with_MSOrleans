using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Infrastructure.Services;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries.Handlers;

internal class EmptyCarsItemCommandHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<EmptyCarsItemCommand>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task Handle(EmptyCarsItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _clusterBaseServices.TryUseGrain<ICarsBookedItemGrain, Task>(
             cart => cart.EmptyCartAsync(),
             () => Task.CompletedTask);

        return Task.CompletedTask;
    }
}