namespace CarsManager.Orleans.Domain.Extensions;
public class CarDetailsDataGenerator
{
    private readonly Dictionary<CarTypes, CarDetails?> _carsStorage = new()
    {
        [CarTypes.Porsche] = CreateCare("1010", "Porsche", "X", "2.4", 2004, "speedy", CarCategory.Sport, 2, 100000, "$", "/Images/Porsche.jpg"),
        [CarTypes.BMW] = CreateCare("1011", "BMW", "C", "2.4", 2022, "modern", CarCategory.Sport, 2, 10000, "$", "/Images/BMW.jpg"),
        [CarTypes.BMW2] = CreateCare("1012", "BMW2", "X", "2.4", 2020, "comfortable", CarCategory.Sport, 2, 10000, "$", "/Images/BMW2.jpg"),
        [CarTypes.Jaguar] = CreateCare("1015", "Jaguar", "X", "2.8", 2021, "extra speedy", CarCategory.Sport, 2, 10000, "$", "/Images/Jaguar.jpg"),

        [CarTypes.AlfaRomeo] = CreateCare("2016", "AlfaRomeo", "P", "2.8", 2021, "sport", CarCategory.Sport, 1, 10000, "$", "/Images/AlfaRomeo.jpg"),
        [CarTypes.Alpine] = CreateCare("2017", "Alpine", "Y", "1.7", 2021, "family", CarCategory.Sport, 3, 10000, "$", "/Images/Alpine.jpg"),
        [CarTypes.Bentley] = CreateCare("2018", "Bentley", "F", "2.4", 2021, "extra speedy", CarCategory.Sport, 1, 10000, "$", "/Images/Bentley.jpg"),
        [CarTypes.Mercedes] = CreateCare("2019", "Mercedes", "G", "3.0", 2021, "extra speedy", CarCategory.Sport, 2, 10000, "$", "/Images/Mercedes.jpg"),
    };

    public List<CarDetails?> GetAllCars()
    {
        return _carsStorage.Values.ToList();
    }

    public CarDetails? GetCar(CarTypes carTypes)
    {
        return _carsStorage.TryGetValue(carTypes, out var value) ? value : new CarDetails();
    }

    private static CarDetails CreateCare(string id, string name, string model, string engine, int year, string desc, CarCategory category, int quant, decimal price, string currency, string imagePath)
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
            Quantity = quant,
            Price = price,
            Currency = currency,
            ImageUrl = imagePath
        };
    }
}
