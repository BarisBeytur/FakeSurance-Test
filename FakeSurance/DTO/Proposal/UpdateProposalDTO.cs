using FakeSurance.Models;

namespace FakeSurance.DTO.Proposal
{
    public class UpdateProposalDTO
    {
        public int ProposalId { get; set; }
        public int ProductId { get; set; }
        public decimal NetPremium { get; set; }
        public decimal GrossPremium { get; set; }
        public int InstallmentCount { get; set; }
        public int PaymentTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime ProposalDate { get; set; }
        public bool IsPolicied { get; set; }
        public DateTime PolicyDate { get; set; }
    }
}
