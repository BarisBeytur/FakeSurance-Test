using System.ComponentModel.DataAnnotations;

namespace FakeSurance.Models
{
    public class Customer
    {
        [Key] 
        public int CustomerId { get; set; }
        public string IdentityNo { get; set; }
        public int IdentityTypeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }


        public Proposal Proposal { get; set; }

    }
}
