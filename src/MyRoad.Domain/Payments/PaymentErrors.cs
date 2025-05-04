using ErrorOr;

namespace MyRoad.Domain.Payments
{
    public static class PaymentErrors
    {
        public static Error InvalidAmount => Error.Validation(
            code: "Payment.InvalidAmount",
            description: "The paid amount is invalid."
        );
        
        public static Error NotFound => Error.NotFound(
            code: "Payment.NotFound",
            description: "can't find this payment"
        );
        
        public static Error IsDeleted => Error.Validation(
            code: "Payment.AlreadyDeleted",
            description: "The Payment has already been deleted.");
    }
}