using ErrorOr;

namespace MyRoad.Domain.Payments
{
    public static class PaymentErrors
    {
        public static Error PaymentExceedsDue => Error.Validation(
            code: "Payment.ExceedsDue",
            description: "The payment amount exceeds the total due."
        );

        public static Error UpdatedPaymentExceedsDue => Error.Validation(
            code: "Payment.UpdatedPaymentExceedsDue",
            description: "The updated payment amount exceeds the total due amount."
        );
        
        public static Error NoDueAmountLeft => Error.Validation(
            code: "Payment.NoDueAmountLeft",
            description: "There is no due amount left to pay."
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