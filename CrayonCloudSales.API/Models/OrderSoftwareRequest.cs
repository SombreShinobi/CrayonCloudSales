namespace CrayonCloudSales.API.Models
{
    public class OrderSoftwareRequest
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public int SoftwareServiceId { get; set; }
        public int Quantity { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
