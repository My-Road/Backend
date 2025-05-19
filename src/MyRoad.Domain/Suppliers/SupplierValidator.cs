using FluentValidation;

namespace MyRoad.Domain.Suppliers
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name cannot be empty");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number cannot be empty")
                .MaximumLength(15);
        }
    }
}
