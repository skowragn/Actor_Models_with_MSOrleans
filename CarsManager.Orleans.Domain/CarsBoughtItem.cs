using System.Text.Json.Serialization;

namespace CarsManager.Orleans.Domain;

[GenerateSerializer, Immutable]
public sealed record class CarsBoughtItem
(
    string UserId,
    int Quantity,
    CarDetails Car)
{
    [JsonIgnore]
    public decimal TotalPrice =>
        Math.Round(Quantity * Car.Price, 2);
}
