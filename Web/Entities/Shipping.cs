namespace Web.Entities
{
    public class Shipping
    {
        public int ShippingID { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public ShippingDetails Details { get; set; }
    }
}
