using System.ComponentModel.DataAnnotations.Schema;

namespace CrayonCloudSales.Core.Models
{
    public class SoftwareService
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerLicense { get; set; }
        public bool IsAvailable { get; set; }
    }
}
