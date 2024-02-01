using CarsManager.Orleans.Core;
using CarsManager.Orleans.Core.Interfaces.Grains;
using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans;

namespace CarsManager.Orleans.Infrastructure.Services;

    public sealed class CarReservationService : BaseClusterService 
    {
        public CarReservationService(
            IHttpContextAccessor httpContextAccessor, IClusterClient client) : base(httpContextAccessor, client) { }

        public async Task<HashSet<CarDetails>> GetAllCarReservationsAsync()
        {
            var getAllProductsTasks = Enum.GetValues<CarCategory>()
                .Select(category =>
                    _client.GetGrain<ICarReservationGrain>(category.ToString()))
                .Select(grain => grain.GetAllCarsAsync())
                .ToList();

            var allProducts = await Task.WhenAll(getAllProductsTasks);

            return new HashSet<CarDetails>(allProducts.SelectMany(cars => cars));
        }

}