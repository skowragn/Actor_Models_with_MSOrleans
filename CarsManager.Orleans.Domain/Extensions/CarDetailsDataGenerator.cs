namespace CarsManager.Orleans.Domain.Extensions;
public class CarDetailsDataGenerator
{
    private readonly Dictionary<CarTypes, CarDetails?> _carsStorage = new()
    {
        [CarTypes.Porsche] = CreateCare("1010", CarTypes.Porsche.ToString(), "X", "2.4", 2004, "speedy", CarCategory.Sport, 1, 100000, "$", "/Images/Porsche.jpg"),
        [CarTypes.BMW] = CreateCare("1011", CarTypes.BMW.ToString(), "C", "2.4", 2022, "modern", CarCategory.SUV, 2, 10000, "$", "/Images/BMW.jpg"),
        [CarTypes.BMW2] = CreateCare("1012", CarTypes.BMW2.ToString(), "X", "2.4", 2020, "comfortable", CarCategory.Sport, 1, 10000, "$", "/Images/BMW2.jpg"),
        [CarTypes.Jaguar] = CreateCare("1015", CarTypes.Jaguar.ToString(), "X", "2.8", 2021, "extra speedy", CarCategory.Other, 1, 10000, "$", "/Images/Jaguar.jpg"),

        [CarTypes.AlfaRomeo] = CreateCare("2016", CarTypes.AlfaRomeo.ToString(), "P", "2.8", 2021, "sport", CarCategory.Hatchback, 1, 10000, "$", "/Images/AlfaRomeo.jpg"),
        [CarTypes.Alpine] = CreateCare("2017", CarTypes.Alpine.ToString(), "Y", "1.7", 2021, "family", CarCategory.Other, 1, 10000, "$", "/Images/Alpine.jpg"),
        [CarTypes.Bentley] = CreateCare("2018", CarTypes.Bentley.ToString(), "F", "2.4", 2021, "extra speedy", CarCategory.Sport, 1, 10000, "$", "/Images/Bentley.jpg"),
        [CarTypes.Mercedes] = CreateCare("2019", CarTypes.Mercedes.ToString(), "G", "3.0", 2021, "extra speedy", CarCategory.Sport, 1, 10000, "$", "/Images/Mercedes.jpg"),
    };
public List<CarDetails?> GetAllCars()
    {
        return _carsStorage.Values.ToList();
    }

    public CarDetails? GetCar(CarTypes carTypes)
    {
        return _carsStorage.TryGetValue(carTypes, out var value) ? value : new CarDetails();
    }

    private static CarDetails CreateCare(string id, string name, string model, string engine, int year, string desc, 
                                         CarCategory category, int quantity, decimal price, string currency, string imagePath)
    {
        return new CarDetails()
        {
            Id = id,
            Name = name,
            Model = model,
            Engine = engine,
            Year = year,
            Description = desc,
            Category = category,
            Quantity = quantity,
            Price = price,
            Currency = currency,
            ImageUrl = imagePath
        };
    }
}
