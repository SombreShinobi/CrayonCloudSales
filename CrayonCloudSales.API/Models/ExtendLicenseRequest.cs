namespace CrayonCloudSales.API.Models
{
    public class ExtendLicenseRequest
    {
        public int CustomerId { get; set; }
        public DateTime NewValidToDate { get; set; }
    }
}
