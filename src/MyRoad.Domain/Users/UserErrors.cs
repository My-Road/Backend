using ErrorOr;

namespace MyRoad.Domain.Users;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        "User.NotFound",
        "Not Found User with this Email");

    public static Error EmailExists => Error.Conflict(
        "User.EmailExists",
        "User Email Already Exists"
    );
    
    public static Error InvalidCredentials => Error.Unauthorized(
        code: "User.InvalidCredentials",
        description: "Invalid email or password."
    );

    public static Error GenericError(string details) => Error.Failure(
        code: "User.GenericError",
        description: details
    );
}