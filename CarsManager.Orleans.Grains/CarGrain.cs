using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans.Runtime;

namespace CarsManager.Orleans.Grains;

public class CarGrain : Grain, ICarGrain
{
    private readonly IPersistentState<CarDetails> _car;
    
    public CarGrain(
        [PersistentState(
            stateName: "Car",
            storageName: "car-reservations")]
        IPersistentState<CarDetails> car) => _car = car;

   Task<int> ICarGrain.GetCarAvailabilityAsync() =>
        Task.FromResult(_car.State.Quantity);

    Task<CarDetails> ICarGrain.GetCarDetailsAsync() =>
        Task.FromResult(_car.State);

    Task ICarGrain.ReturnCarAsync(int quantity) =>
        UpdateStateAsync(_car.State with
        {
            Quantity = _car.State.Quantity + quantity
        });

    async Task<(bool IsAvailable, CarDetails? CarDetails)> ICarGrain.TryTakeCarAsync(int quantity)
    {
        if (_car.State.Quantity < quantity)
        {
            return (false, null);
        }

        var updatedState = _car.State with
        {
            Quantity = _car.State.Quantity - quantity
        };

        await UpdateStateAsync(updatedState);

        return (true, _car.State);
    }

    Task ICarGrain.CreateOrUpdateCarAsync(CarDetails productDetails) =>
        UpdateStateAsync(productDetails);

    private async Task UpdateStateAsync(CarDetails car)
    {
        var oldCategory = _car.State.Category;

        _car.State = car;
        await _car.WriteStateAsync();

        var inventoryGrain = GrainFactory.GetGrain<ICarInventoryGrain>(_car.State.Category.ToString());
        await inventoryGrain.AddOrUpdateCarAsync(car);

        if (oldCategory != car.Category)
        {
            var oldInventoryGrain = GrainFactory.GetGrain<ICarInventoryGrain>(oldCategory.ToString());
            await oldInventoryGrain.RemoveCarAsync(car.Id);
        }
    }
}