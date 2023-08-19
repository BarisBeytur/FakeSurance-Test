using FakeSurance.Models;

namespace FakeSurance.DTO.ProductCoverage
{
    public class CreateProductCoverageDTO
    {
        public int ProductCoverageId { get; set; }
        public int CoverageId { get; set; }
        public int ProductId { get; set; }
    }
}
