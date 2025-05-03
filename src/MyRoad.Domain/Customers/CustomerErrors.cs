using ErrorOr;

namespace MyRoad.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Customer.NotFound",
        description: "can't find this customer."
    );

    public static Error NotDeleted => Error.Validation(
        code: "Customer.NotDeleted",
        description: "Customer is already active."
    );

    public static Error AlreadyDeleted => Error.Validation(
        code: "Customer.AlreadyDeleted",
        description: "Customer is already deleted."
    );

    public static Error CannotRemoveOrder => Error.Conflict(
        code: "Customer.CannotRemoveOrder",
        description:
        "Cannot remove this order because it would result in overpayment or the customer has already fully paid."
    );

    public static Error CannotUpdateOrder =>
        Error.Conflict(
            code: "Customer.CannotUpdateOrder",
            description:
            "Cannot update the order because the customer's paid amount would exceed their total due amount.");
}