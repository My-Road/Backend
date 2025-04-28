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
}