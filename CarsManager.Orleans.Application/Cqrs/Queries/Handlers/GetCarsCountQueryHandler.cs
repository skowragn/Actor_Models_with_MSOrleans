using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Application.Services;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Queries.Handlers;

internal class GetCarsCountQueryHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<GetCarsCountQuery, int>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task<int> Handle(GetCarsCountQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var carCount = _clusterBaseServices.TryUseGrain<ICarsBoughtGrain, Task<int>>(
           cart => cart.GetTotalItemsInCartAsync(),
           () => Task.FromResult(0));

        return carCount;

    }
}