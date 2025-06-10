using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Reports;
using Sieve.Models;

namespace MyRoad.Domain.Purchases
{
    public interface IPurchaseService
    {
        Task<ErrorOr<Success>> CreateAsync(Purchase purchase);

        Task<ErrorOr<Success>> UpdateAsync(Purchase purchase);

        Task<ErrorOr<Success>> DeleteAsync(long id);

        Task<ErrorOr<Purchase>> GetByIdAsync(long id);

        Task<ErrorOr<PaginatedResponse<Purchase>>> GetBySupplierIdAsync(long supplierId, SieveModel sieveModel);

        Task<ErrorOr<PaginatedResponse<Purchase>>> GetAsync(SieveModel sieveModel);

        Task<ErrorOr<List<Purchase>>> GetPurchasesForReportAsync(ReportFilter filter);
    }
}