using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Application.Services;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Queries.Handlers;

internal class GetAllBoughtCarsItemsQueryHandler(ClusterBaseServices clusterBaseServices) : IRequestHandler<GetAllBoughtCarsItemsQuery, HashSet<CarsBoughtItem>>
{
    private readonly ClusterBaseServices _clusterBaseServices = clusterBaseServices;

    public async Task<HashSet<CarsBoughtItem>> Handle(GetAllBoughtCarsItemsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var carsBookedItems = await _clusterBaseServices.TryUseGrain<ICarsBoughtGrain, Task<HashSet<CarsBoughtItem>>>(
          cart => cart.GetAllItemsAsync(),
          () => Task.FromResult(new HashSet<CarsBoughtItem>()));

        return carsBookedItems;
    }
}