using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Application.Services;
using CarsManager.Orleans.Application.Cqrs.Commands;
using MediatR;

namespace CarsManager.Orleans.Application.Extensions.Cqrs.Queries.Handlers;

internal class EmptyBoughtCarsItemCommandHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<EmptyBoughtCarsItemCommand>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task Handle(EmptyBoughtCarsItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _clusterBaseServices.TryUseGrain<ICarsBoughtGrain, Task>(
             cart => cart.EmptyCartAsync(),
             () => Task.CompletedTask);

        return Task.CompletedTask;
    }
}