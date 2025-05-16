using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Payments.SupplierPayments
{
    public interface ISupplierPaymentService
    {
        Task<ErrorOr<Success>> CreateAsync(SupplierPayment supplierPayment);

        Task<ErrorOr<Success>> UpdateAsync(SupplierPayment supplierPayment);

        Task<ErrorOr<Success>> DeleteAsync(long id);

        Task<ErrorOr<SupplierPayment>> GetByIdAsync(long id);

        Task<ErrorOr<PaginatedResponse<SupplierPayment>>> GetBySupplierIdAsync(long supplierId, SieveModel sieveModel);

        Task<ErrorOr<PaginatedResponse<SupplierPayment>>> GetAsync(SieveModel sieveModel);
    }
}
