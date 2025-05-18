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
    
    public static Error PhoneNumberAlreadyExists => Error.Conflict(
        code: "Customer.PhoneNumberExists",
        description: "Customer with phone number already exists"
    );

    public static Error EmailAlreadyExists => Error.Conflict(
        code: "Customer.EmailExists",
        description: "Customer with email already exists"
    );
}