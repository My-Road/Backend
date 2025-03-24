using ErrorOr;

namespace MyRoad.Domain.Identity;

public static class IdentityErrors
{
    public static Error GenericError(string message) => Error.Failure(
        code: "Identity.GenericError",
        description: message
    );

    public static Error WrongCurrentPassword => Error.Conflict(
        code: "Identity.WrongCurrentPassword",
        description: "Current password is wrong"
    );
}