using ErrorOr;

namespace MyRoad.Domain.Email;

public static class EmailErrors
{
    public static Error NoRecipient() => Error.Validation(
        code: "Email.NoRecipient",
        description: "Email must have at least one recipient."
    );

    public static Error SmtpError(string details) => Error.Failure(
        code: "Email.SmtpError",
        description: $"SMTP error occurred: {details}"
    );

    public static Error GenericError(string details) => Error.Failure(
        code: "Email.GenericError",
        description: details
    );
}