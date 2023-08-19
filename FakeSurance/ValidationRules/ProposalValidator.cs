using FakeSurance.DTO.Proposal;
using FakeSurance.Models;
using FluentValidation;

namespace FakeSurance.ValidationRules
{
    public class ProposalValidator : AbstractValidator<ProposalApplicationDTO>
    {
        public ProposalValidator()
        {

            RuleFor(x => x.identityNo).NotEmpty().WithMessage("Kimlik numarasını boş geçemezsiniz!!")
                .MaximumLength(11).WithMessage("Kimlik numarasını maksimum 11 hane olmalıdır!");

            RuleFor(x => x.chassisNo).NotEmpty().WithMessage("Şasi numarasını boş geçemezsiniz!");

            RuleFor(x => x.kod).NotEmpty().WithMessage("Kod alanını boş geçemezsiniz!");

            RuleFor(x => x.installmentCount).NotNull().WithMessage("Ödeme sıklığını boş geçemezsiniz! 0:Peşin 1:Taksit");

            RuleFor(x => x.paymentTypeId).NotEmpty().WithMessage("Ödeme Tipini boş geçemezsiniz! 1:Havale 2:Kredi Kartı");

            RuleFor(x => x.paymentMethodId).NotEmpty().WithMessage("Ödeme metodunu boş geçemezsiniz! 1:SanalPOS 2:3DSecure");

        }
    }
}
