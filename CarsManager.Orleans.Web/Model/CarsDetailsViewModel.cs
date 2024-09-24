namespace CarsManager.Orleans.Web.Model;
public class CarsDetailsViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string Engine { get; set; } = null!;
    public int Year { get; set; }
    public string Description { get; set; } = null!;
    public CarCategoryViewModel Category { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
