using CarsManager.Orleans.Domain.Extensions;
using CarsManager.Orleans.Domain.Interfaces.Grains;
using Orleans.Runtime;

namespace CarsManager.Orleans.Infrastructure.StartupTasks;

public sealed class SeedCarsTask : IStartupTask
{
    private readonly IGrainFactory _grainFactory;

    public SeedCarsTask(IGrainFactory grainFactory) =>
        _grainFactory = grainFactory;

    async Task IStartupTask.Execute(CancellationToken cancellationToken)
    {            
       var cars = new CarDetailsDataGenerator().GetAllCars();

       foreach (var car in cars)
       {
           var carGrain = _grainFactory.GetGrain<ICarGrain>(car.Id);
           await carGrain.CreateOrUpdateCarAsync(car);
       }
    }
}
