using ErrorOr;

namespace MyRoad.Domain.Payments
{
    public static class PaymentErrors
    {
        public static Error InvalidAmount => Error.Validation(
            code: "Payment.InvalidAmount",
            description: "The paid amount is invalid."
        );

        public static Error SomethingWrong => Error.Failure(
            code: "Payment.Failure",
            description: "can't record this payment something wrong"
        );

        public static Error NotFound => Error.NotFound(
            code: "Payment.NotFound",
            description: "can't find this payment"
        );
    }
}