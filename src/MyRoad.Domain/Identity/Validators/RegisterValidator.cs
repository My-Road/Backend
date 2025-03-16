using FluentValidation;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(x => x is "Admin" or "Manager")
            .WithMessage("Role must be either Admin or Manager");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(14);
        
    }
}