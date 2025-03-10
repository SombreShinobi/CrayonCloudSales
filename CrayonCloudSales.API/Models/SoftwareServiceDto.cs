using System.ComponentModel.DataAnnotations.Schema;

namespace CrayonCloudSales.API.Models
{
    public class SoftwareServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerLicense { get; set; }
    }
}
