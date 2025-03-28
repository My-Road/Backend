using FluentValidation;

namespace MyRoad.Domain.Identity.Validators;

public class ForgotPasswordValidator : AbstractValidator<ForgetPasswordRequestDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Current password is required")
            .Equal(u => u.ConfirmNewPassword)
            .WithMessage("Current passwords does not match");
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("New password must contain at least one digit")
            .Matches(@"[\W]").WithMessage("New password must contain at least one special character");
    }
}

public class ForgetPasswordRequestDto(string newPassword, string confirmNewPassword)
{
    public string NewPassword { get; set; } = newPassword;
    public string ConfirmNewPassword { get; set; } = confirmNewPassword;
}