using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Services.Interfaces;
public interface ICarReservationsService
{
    Task<IEnumerable<CarsDetailsViewModel>?> GetAllCarReservations();
    Task AddOrUpdateItem(int quantity, CarsDetailsViewModel car);
    Task CreateOrUpdateCar(CarsDetailsViewModel car);
}