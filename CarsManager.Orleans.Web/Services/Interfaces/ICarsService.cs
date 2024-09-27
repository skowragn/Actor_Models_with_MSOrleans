using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Services.Interfaces;
public interface ICarsService
{
    Task CreateOrUpdateAvailableCarDetails(CarsDetailsViewModel car);
}