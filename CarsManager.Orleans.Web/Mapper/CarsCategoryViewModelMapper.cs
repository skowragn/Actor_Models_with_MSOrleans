using CarsManager.Orleans.Domain;
using CarsManager.Orleans.Web.Model;

namespace CarsManager.Orleans.Web.Mapper;

public static class CarsCategoryViewModelMapper
{
   public static CarCategory ToCarCategory(this CarCategoryViewModel carCategoryViewModel)
   {
        return (carCategoryViewModel.CategoryName != null) ?
               (CarCategory) Enum.Parse(typeof(CarCategory), carCategoryViewModel.CategoryName, true) :
               CarCategory.Other;
    }

    public static CarCategoryViewModel ToCarCategoryViewModel(this CarCategory carCategory)
    {

        return new CarCategoryViewModel
        {
            CategoryName = carCategory.ToString(),
            Description = carCategory.ToString()
        };
    }
}
