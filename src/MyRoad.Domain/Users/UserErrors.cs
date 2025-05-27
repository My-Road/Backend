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

    public static Error InvalidCredentials => Error.Unauthorized(
        code: "User.InvalidCredentials",
        description: "Invalid email or password."
    );

    public static Error UnauthorizedUser => Error.Unauthorized(
        code: "User.Unauthorized",
        description: "this user Unauthorized"
    );
}