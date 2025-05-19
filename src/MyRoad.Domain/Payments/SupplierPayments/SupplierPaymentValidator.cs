using FluentValidation;

namespace MyRoad.Domain.Payments.SupplierPayments
{
    public class SupplierPaymentValidator : AbstractValidator<SupplierPayment>
    {
        public SupplierPaymentValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty()
                .WithMessage("Supplier Id cannot be empty");
        }
    }
}
