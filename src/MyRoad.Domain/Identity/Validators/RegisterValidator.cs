using FluentValidation;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Validators;

public class RegisterValidator : AbstractValidator<User>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(role => role.Equals(UserRole.Admin) || role.Equals(UserRole.Manager))
            .WithMessage("Role must be either Admin or Manager");
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(14);
    }
}