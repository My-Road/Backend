using MyRoad.API.Employees.ResponseDto;
using MyRoad.API.Payments.ResponseDto;
using MyRoad.API.Suppliers.ResponseDto;

namespace MyRoad.API.Payments.SupplierPayments.ResponseDto;

public class SupplierPaymentResponseDto : PaymentResponseDto
{
    public SupplierDto Supplier { get; set; }
}