using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Queries.Handlers;

internal class GetAllAvailableCarsDetailsQueryHandler(IClusterClient client) : IRequestHandler<GetAllAvailableCarsDetailsQuery, HashSet<CarDetails>>
{
    private readonly IClusterClient _client = client;

    public async Task<HashSet<CarDetails>> Handle(GetAllAvailableCarsDetailsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var getAllProductsTasks = Enum.GetValues<CarCategory>()
            .Select(category =>
                _client.GetGrain<ICarInventoryGrain>(category.ToString()))
            .Select(grain => grain.GetAllCarsAsync())
            .ToList();

        var allProducts = await Task.WhenAll(getAllProductsTasks);

        return new HashSet<CarDetails>(allProducts.SelectMany(cars => cars));
    }
}