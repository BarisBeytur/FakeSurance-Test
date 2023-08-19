using FakeSurance.Models;

namespace FakeSurance.DTO.Proposal
{
    public class ProposalApplicationDTO
    {
        public string identityNo { get; set; }
        public string chassisNo { get; set; }
        public int kod { get; set; }
        public int paymentTypeId { get; set; }
        public int paymentMethodId { get; set; }
        public int installmentCount { get; set; }

    }
}
