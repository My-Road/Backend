using System.Data;
using FluentValidation;
using MyRoad.Domain.Identity.RequestsDto;

namespace MyRoad.Domain.Identity.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequestDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required")
            .NotEqual(u => u.NewPassword)
            .WithMessage("Current password must be not same as new password");


        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("New password must contain at least one digit")
            .Matches(@"[\W]").WithMessage("New password must contain at least one special character");
    }
}