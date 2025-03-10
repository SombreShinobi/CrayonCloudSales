namespace CrayonCloudSales.Core.Models
{

    public enum LicenseState
    {
        Active,
        Inactive,
        Canceled,
        Expired
    }

    public class SoftwareLicense
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int SoftwareServiceId { get; set; }
        public int Quantity { get; set; }
        public LicenseState State { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ValidToDate { get; set; }

        public string CcpSubscriptionId { get; set; } = string.Empty;

        public Account? Account { get; set; }
    }

}
