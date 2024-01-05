namespace Web.Entities;

public class Orderline
{
    public int OrderLineID { get; set; }
    public decimal Price { get; set; }
    public int SKU { get; set; }
    public int OrderID { get; set; }
    public int ShippingID { get; set; }
}