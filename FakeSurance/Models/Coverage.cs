using System.ComponentModel.DataAnnotations;

namespace FakeSurance.Models
{
    public class Coverage
    {
        [Key]
        public int CoverageId { get; set; }
        public string Name { get; set; }
    }
}
