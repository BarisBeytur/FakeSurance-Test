using System.ComponentModel.DataAnnotations;

namespace FakeSurance.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Kod { get; set; }
        public int ProductTypeId { get; set; }


        public ICollection<Proposal> Proposal { get; set; }
    }
}
