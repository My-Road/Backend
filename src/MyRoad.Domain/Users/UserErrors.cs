using ErrorOr;

namespace MyRoad.Domain.Users;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "Not Found User with this Email");

    public static Error NotFoundId => Error.NotFound(
        code: "User.NotFoundId",
        description: "Not Found User with this Id"
    );

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