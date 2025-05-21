using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Purchases
{
    public interface IPurchaseRepository
    {
        Task<bool> CreateAsync(Purchase purchase);

        Task<bool> UpdateAsync(Purchase purchase);

        Task<Purchase?> GetByIdAsync(long id);

        Task<PaginatedResponse<Purchase>> GetAsync(SieveModel sieveModel);

        Task<PaginatedResponse<Purchase>> GetBySupplierAsync(long supplierId, SieveModel sieveModel);
    }
}