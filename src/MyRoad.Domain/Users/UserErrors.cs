using ErrorOr;

namespace MyRoad.Domain.Users;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "Not Found this User");

    public static Error EmailExists => Error.Conflict(
        "User.EmailExists",
        "User Email Already Exists"
    );

    public static Error PhoneNumberExists => Error.Conflict(
        code: "User.PhoneNumberExists",
        description: "Phone Number Already Exists"
    );

    public static Error InvalidCredentials => Error.Unauthorized(
        code: "User.InvalidCredentials",
        description: "Invalid email or password."
    );

    public static Error UnauthorizedUser => Error.Unauthorized(
        code: "User.Unauthorized",
        description: "this user Unauthorized"
    );
 
    public static readonly Error CannotToggleOwnStatus = Error.Forbidden(
        code: "User.CannotToggleOwnStatus",
        description: "You cannot toggle your own account status.");
    
    public static readonly Error CannotChangeOwnRole = Error.Forbidden(
        code: "User.CannotChangeOwnRole",
        description: "You cannot change your own role.");

    public static Error UserLocked => Error.Conflict(
        code: "User.Locked",
        description: "The account has been temporarily locked due to a number of incorrect login attempts."
    );
}