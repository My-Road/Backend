using ErrorOr;

namespace MyRoad.Domain.Suppliers
{
    public class SupplierErrors
    {
        public static Error NotFound => Error.NotFound(
            code: "Supplier.NotFound",
            description: "can't find this supplier."
        );

        public static Error AlreadyDeleted => Error.Validation(
            code: "Supplier.AlreadyDeleted",
            description: "Supplier is already deleted."
        );

        public static Error PhoneNumberAlreadyExists => Error.Conflict(
            code: "Supplier .PhoneNumberExists",
            description: "Supplier with phone number already exists"
        );

        public static Error EmailAlreadyExists => Error.Conflict(
            code: "Supplier .EmailExists",
            description: "Supplier  with email already exists"
        );

        public static Error CannotUpdatePurchase => Error.Conflict(
            code: "Customer.CannotUpdatePurchase",
            description:"Cannot update the purchase because the customer's paid amount would exceed their total due amount."
        );

        public static Error NotDeleted => Error.Validation(
            code: "Suppleir.NotDeleted",
            description: "Suppleir is already active."
        );
    }
}
