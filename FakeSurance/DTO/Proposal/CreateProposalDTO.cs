namespace FakeSurance.DTO.Proposal
{
    public class CreateProposalDTO
    {
        public int ProductId { get; set; }
        public decimal NetPremium { get; set; }
        public decimal GrossPremium { get; set; }
        public int InstallmentCount { get; set; }
        public int PaymentTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }

    }
}
