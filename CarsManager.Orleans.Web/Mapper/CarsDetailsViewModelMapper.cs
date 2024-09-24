using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Mapper;

public static class CarsDetailsViewModelMapper
{
    public static CarDetails ToCarDetails(this CarsDetailsViewModel carDetails)
    {
        return new CarDetails()
        {
            Id = carDetails.Id,
            Name = carDetails.Name,
            Model = carDetails.Model,
            Engine = carDetails.Engine,
            Year = carDetails.Year,
            Description = carDetails.Description,
            Category = carDetails.Category.ToCarCategory(),
            Quantity = carDetails.Quantity,
            Price = carDetails.Price,
            Currency = carDetails.Currency,
            ImageUrl = carDetails.ImageUrl
        };
    }

    public static CarsDetailsViewModel ToCarDetailsViewModel(this CarDetails carDetailsRequest)
    {
        return new CarsDetailsViewModel()
        {
            Id = carDetailsRequest.Id,
            Name = carDetailsRequest.Name,
            Model = carDetailsRequest.Model,
            Engine = carDetailsRequest.Engine,
            Year = carDetailsRequest.Year,
            Description = carDetailsRequest.Description,
            Quantity = carDetailsRequest.Quantity,
            Category = carDetailsRequest.Category.ToCarCategoryViewModel(),
            Price = carDetailsRequest.Price,
            Currency = carDetailsRequest.Currency,
            ImageUrl = carDetailsRequest.ImageUrl
        };
    }
}