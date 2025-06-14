using ErrorOr;

namespace MyRoad.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Order.NotFound",
        description: "can't find this Order"
    );

    public static Error InvalidPrice => Error.Validation(
        code: "Order.InvalidPrice",
        description: "The order price must be greater than zero."
    );

    public static Error NotDeleted => Error.Validation(
        code: "Order.NotDeleted",
        description: "Order is already active."
    );

    public static Error IsDeleted => Error.Validation(
        code: "Order.AlreadyDeleted",
        description: "The order has already been deleted.");
    
    public static Error CannotRemoveOrder => Error.Conflict(
        code: "Order.CannotRemoveOrder",
        description:
        "Cannot remove this order because it would result in overpayment or the customer has already fully paid."
    );

    public static Error CannotUpdateOrder =>
        Error.Conflict(
            code: "Order.CannotUpdateOrder",
            description:
            "Cannot update the order because the customer's paid amount would exceed their total due amount.");

    public static Error InvalidDateRange => Error.Validation(
      code: "Order.InvalidDateRange",
      description: "Start date must be earlier than end date");
}