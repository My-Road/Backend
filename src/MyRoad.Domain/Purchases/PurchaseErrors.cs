using ErrorOr;

namespace MyRoad.Domain.Purchases
{
    public class PurchaseErrors
    {
        public static Error NotFound => Error.NotFound(
            code: "Purchase.NotFound",
            description: "can't find this Purchase"
        );


        public static Error InvalidPrice => Error.Validation(
            code: "Purchase.InvalidPrice",
            description: "The Purchase price must be greater than zero."
        );

        public static Error NotDeleted => Error.Validation(
            code: "Purchase.NotDeleted",
            description: "Purchase is already active."
        );

        public static Error IsDeleted => Error.Validation(
            code: "Purchase.AlreadyDeleted",
            description: "The Purchase has already been deleted.");
    }
}
