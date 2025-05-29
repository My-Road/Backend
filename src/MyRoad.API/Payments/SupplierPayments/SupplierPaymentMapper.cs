using MyRoad.API.Payments.SupplierPayments.RequestDto;
using MyRoad.API.Payments.SupplierPayments.ResponseDto;
using MyRoad.Domain.Payments.SupplierPayments;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Payments.SupplierPayments
{
    [Mapper]
    public static partial class SupplierPaymentMapper
    {
        public static partial SupplierPayment ToSupplierPayment(this CreateSupplierPaymentDto dto);
        public static partial SupplierPayment ToSupplierPayment(this UpdateSupplierPaymentDto dto);
        public static partial SupplierPaymentResponseDto ToSupplierPaymentResponseDto(this SupplierPayment dto);
    }
}
