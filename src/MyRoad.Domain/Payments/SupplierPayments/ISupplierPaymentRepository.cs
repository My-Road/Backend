using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Payments.SupplierPayments;
using Sieve.Models;

namespace MyRoad.Domain.Payments.SupplierPayments
{
    public interface ISupplierPaymentRepository
    {
        Task<bool> CreateAsync(SupplierPayment supplierPayment);

        Task<bool> UpdateAsync(SupplierPayment supplierPayment);

        Task<SupplierPayment?> GetByIdAsync(long supplierPaymentId);

        Task<PaginatedResponse<SupplierPayment>> GetAsync(SieveModel sieveModel);

        Task<PaginatedResponse<SupplierPayment>> GetBySupplierIdAsync(long supplierId, SieveModel sieveModel);
    }
}
