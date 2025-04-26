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
    
    public static Error AlreadyExists = Error.Conflict(
        code: "Customer.AlreadyExists",
        description: "A customer with the same ID already exists.");
}