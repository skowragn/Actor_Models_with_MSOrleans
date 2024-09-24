namespace CarsManager.Orleans.Web.Model;

public class CarsBookedItemViewModel
{
    public string UserId { get; set; } = null!;
    public int Quantity { get; set; }
    public CarsDetailsViewModel Car { get; set; } = null!;

    public decimal TotalPrice { get; set; }
}
