using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Queries;
using CarsManager.Orleans.Infrastructure.Services;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries.Handlers;

internal class GetCarsCountQueryHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<GetCarsCountQuery, int>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public Task<int> Handle(GetCarsCountQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var carCount = _clusterBaseServices.TryUseGrain<ICarsBookedItemGrain, Task<int>>(
           cart => cart.GetTotalItemsInCartAsync(),
           () => Task.FromResult(0));

        return carCount;

    }
}