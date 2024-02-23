using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Infrastructure.Services;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries.Handlers;

internal class GetAllBookedCarsItemsQueryHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<GetAllBookedCarsItemsQuery, HashSet<CarsBookedItem>>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public async Task<HashSet<CarsBookedItem>> Handle(GetAllBookedCarsItemsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var carsBookedItems = await _clusterBaseServices.TryUseGrain<ICarsBookedItemGrain, Task<HashSet<CarsBookedItem>>>(
          cart => cart.GetAllItemsAsync(),
          () => Task.FromResult(new HashSet<CarsBookedItem>()));

        return carsBookedItems;
    }
}