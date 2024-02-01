using System.Text.Json.Serialization;

namespace CarsManager.Orleans.Domain;


[GenerateSerializer, Immutable]
public sealed record CarDetails
{
    [Id(0)] public string Id { get; set; } = null!;

    [Id(1)] public string Name { get; set; } = null!;

    [Id(2)] public string Model { get; set; } = null!;

    [Id(3)] public string Engine { get; set; } = null!;

    [Id(4)] public int Year { get; set; }

    [Id(5)] public string Description { get; set; } = null!;

    [Id(6)] public CarCategory Category { get; set; }
    [Id(7)] public int Quantity { get; set; }
    [Id(8)] public decimal Price { get; set; }

    [Id(9)] public string Currency { get; set; } = null!;
    [Id(10)] public string ImageUrl { get; set; } = null!;
    

    [JsonIgnore]
    public decimal TotalPrice => Math.Round(Quantity * Price, 2);
}


/*[Immutable]
public sealed record class CarDetails
{
    public string Id { get; set; } = null!;
   public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public CarCategory Category { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string DetailsUrl { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public DateTime ProductionDate { get; set; } = DateTime.Now;

    [JsonIgnore]
    public decimal TotalPrice =>
        Math.Round(Quantity * Price, 2);

}*/