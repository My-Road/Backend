using ErrorOr;

namespace MyRoad.Domain.Identity;

public static class IdentityErrors
{
    public static Error GenericError(string message) => Error.Failure(
        code: "Identity.GenericError",
        description: message
    );

    public static Error FailedToResetPassword => Error.Failure(
        code: "Identity.FailedToResetPassword",
        description: "An error occurred while resetting the password. Please try again.");


    public static Error InvalidResetPasswordToken => Error.Unauthorized(
        code: "Identity.InvalidResetPasswordToken",
        description: "Invalid reset password token provided.");

    public static Error WrongCurrentPassword => Error.Conflict(
        code: "Identity.WrongCurrentPassword",
        description: "Current password is wrong"
    );
}