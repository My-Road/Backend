
using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Suppliers
{
    public interface ISupplierService
    {
        Task<ErrorOr<Success>> CreateAsync(Supplier supplier);

        Task<ErrorOr<Success>> UpdateAsync(Supplier supplier);

        Task<ErrorOr<Success>> DeleteAsync(long id);

        Task<ErrorOr<Supplier>> GetByIdAsync(long id);

        Task<ErrorOr<PaginatedResponse<Supplier>>> GetAsync(SieveModel sieveModel);

        Task<ErrorOr<Success>> RestoreAsync(long id);
    }
}
