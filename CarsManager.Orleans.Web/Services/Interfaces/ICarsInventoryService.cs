using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Services.Interfaces;
public interface ICarsInventoryService
{
    Task<IEnumerable<CarsDetailsViewModel>?> GetAllAvailableCarsDetails();
}