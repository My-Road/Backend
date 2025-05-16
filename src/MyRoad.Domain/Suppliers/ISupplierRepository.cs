using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Suppliers
{
    public interface ISupplierRepository
    {
        Task<bool> CreateAsync(Supplier supplier);

        Task<bool> UpdateAsync(Supplier supplier);

        Task<Supplier?> GetByIdAsync(long supplierId);

        Task<ErrorOr<PaginatedResponse<Supplier>>> GetAsync(SieveModel sieveModel);

        Task<Supplier?> FindByPhoneNumber(string? supplierPhoneNumber);
        Task<Supplier?> FindByEmail(string? supplierEmail);
    }
}
