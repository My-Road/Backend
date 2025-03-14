using FluentValidation;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MaximumLength(10);
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress();
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(x => x is "Admin" or "Manager")
            .WithMessage("Role is Admin or Manager");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8);
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("ConfirmPassword is required")
            .Equal(y => y.ConfirmPassword)
            .WithMessage("ConfirmPassword does not match");

    }
}