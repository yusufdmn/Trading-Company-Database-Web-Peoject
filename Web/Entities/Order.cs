namespace Web.Entities;

public class Order
{
    public int OrderID { get; set; }
    public string BillURL { get; set; }
    public decimal TotalPrice{ get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerID { get; set; }
    public string PlatformURL { get; set; }
}