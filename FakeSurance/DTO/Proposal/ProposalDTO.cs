using FakeSurance.Models;

namespace FakeSurance.DTO.Proposal
{
    public class ProposalDTO
    {
        public int ProposalId { get; set; }
        public string ProductName { get; set; }
        public decimal NetPremium { get; set; }
        public decimal GrossPremium { get; set; }
        public int InstallmentCount { get; set; }
        public string InstallmentCountName { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerIdentityNo { get; set; }
        public int VehicleId { get; set; }
        public int PlateCity { get; set; }
        public string PlateDetail { get; set; }
        public string MotorNo { get; set; }
        public string ChassisNo { get; set; }
        public int ManufactureYear { get; set; }
        public string BrandCode { get; set; }
        public string ModelCode { get; set; }
        public DateTime TrafficStartDate { get; set; }
        public DateTime ProposalDate { get; set; }
        public bool IsPolicied { get; set; }
    }

    public enum installmentCount
    {
        Pesin = 0,
        Taksit = 1
    }

    public enum paymentType
    {
        Havale = 1,
        KrediKarti = 2
    }

    public enum paymentMethod
    {
        SanalPos = 1,
        Secure3DS = 2
    }


}
