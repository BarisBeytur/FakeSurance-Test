namespace FakeSurance.DTO.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string IdentityNo { get; set; }
        public int IdentityTypeId { get; set; }
        public string? IdentityTypeName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public enum identityType
    {
        TCKN = 1,
        VKN = 2,
        YKN = 3
    }
}
