using ErrorOr;

namespace MyRoad.Domain.Purchases
{
    public static class PurchaseErrors
    {
        public static Error NotFound => Error.NotFound(
            code: "Purchase.NotFound",
            description: "can't find this Purchase"
        );


        public static Error InvalidPrice => Error.Validation(
            code: "Purchase.InvalidPrice",
            description: "The Purchase price must be greater than zero."
        );

        public static Error IsDeleted => Error.Validation(
            code: "Purchase.AlreadyDeleted",
            description: "The Purchase has already been deleted.");
        
        
        public static Error CannotUpdatePurchase => Error.Conflict(
            code: "Purchase.CannotUpdatePurchase",
            description:
            "The purchase cannot be updated because the amount paid to the supplier would exceed the total amount owed."
        );

        public static Error CannotRemovePurchase => Error.Conflict(
            code: "Purchase.CannotRemovePurchase",
            description:
            "This Purchase cannot be removed because it will result in an overpayment or the full amount has already been paid."
        );

    }
}
