using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains;

[Reentrant]
public sealed class CarsInventoryGrain : Grain, ICarInventoryGrain
{
    private readonly Dictionary<string, CarDetails> _carCache = [];
    private readonly IPersistentState<HashSet<string>> _state;

    public CarsInventoryGrain(
       [PersistentState(
            stateName: "Inventory",
            storageName: "car-reservations")]
        IPersistentState<HashSet<string>> state)
    {
        _state = state;
    }

    public override Task OnActivateAsync(CancellationToken token) => PopulateProductCacheAsync(token);
    
    public Task<HashSet<CarDetails>> GetAllCarsAsync() => Task.FromResult(_carCache.Values.ToHashSet());

    public async Task AddOrUpdateCarAsync(CarDetails car)
    {
        _state.State.Add(car.Id);
        _carCache[car.Id] = car;

        await _state.WriteStateAsync();
    }

    public async Task RemoveCarAsync(string carId)
    {
        _state.State.Remove(carId);
        _carCache.Remove(carId);

        await _state.WriteStateAsync();
    }

    private async Task PopulateProductCacheAsync(CancellationToken token)
    {
        if (_state is not { State.Count: > 0 })
        {
            return;
        }

        await Parallel.ForEachAsync(
            _state.State,
            async (id, _) =>
            {
                var carGrain = GrainFactory.GetGrain<ICarGrain>(id);
                _carCache[id] = await carGrain.GetCarDetailsAsync();
            });
    }
}