using ErrorOr;

namespace MyRoad.Domain.Email;

public static class EmailErrors
{
    public static Error NoRecipient() => Error.Validation(
        code: "Email.NoRecipient",
        description: "Email must have at least one recipient."
    );
}