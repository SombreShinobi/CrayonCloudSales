namespace CrayonCloudSales.API.Models
{
    public class SoftwareLicenseDto
    {
        public int Id { get; set; }
        public string SoftwareName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string State { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
