using System.ComponentModel.DataAnnotations;

namespace FakeSurance.Models
{
    public class ProductCoverage
    {
        [Key]
        public int ProductCoverageId { get; set; }

        public int? CoverageId { get; set; }
        public Coverage Coverage { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
