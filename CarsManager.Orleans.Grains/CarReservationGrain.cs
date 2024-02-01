using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains;

[Reentrant]
public sealed class CarReservationGrain : Grain, ICarReservationGrain
{
    private readonly IPersistentState<HashSet<string>> _carsIds;
    private readonly Dictionary<string, CarDetails> _carCache = [];

    public CarReservationGrain(
        [PersistentState(
            stateName: "reservation",
            storageName: "car-reservations")]
        IPersistentState<HashSet<string>> state) => _carsIds = state;

    public override Task OnActivateAsync(CancellationToken token) => PopulateProductCacheAsync(token);
    
    Task<HashSet<CarDetails>> ICarReservationGrain.GetAllCarsAsync() =>
        Task.FromResult(_carCache.Values.ToHashSet());

    async Task ICarReservationGrain.AddOrUpdateCarAsync(CarDetails car)
    {
        _carsIds.State.Add(car.Id);
        _carCache[car.Id] = car;

        await _carsIds.WriteStateAsync();
    }

    public async Task RemoveCarAsync(string carId)
    {
        _carsIds.State.Remove(carId);
        _carCache.Remove(carId);

        await _carsIds.WriteStateAsync();
    }

    private async Task PopulateProductCacheAsync(CancellationToken token)
    {
        if (_carsIds is not { State.Count: > 0 })
        {
            return;
        }

        await Parallel.ForEachAsync(
            _carsIds.State,
            async (id, _) =>
            {
                var carGrain = GrainFactory.GetGrain<ICarGrain>(id);
                _carCache[id] = await carGrain.GetCarDetailsAsync();
            });
    }
}