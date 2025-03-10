namespace CrayonCloudSales.API.Models
{
    public class UpdateQuantityRequest
    {
        public int CustomerId { get; set; }
        public int NewQuantity { get; set; }
    }
}
