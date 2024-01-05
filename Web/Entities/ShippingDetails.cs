namespace Web.Entities
{
    public class ShippingDetails
    {
        public int SDetailID {get; set; }
        public int ShippingID { get; set; }
        public string Company { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Country { get; set; }
        public string? State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
    
}
