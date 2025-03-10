namespace CrayonCloudSales.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<SoftwareLicense>? SoftwareLicenses { get; set; }
    }
}
