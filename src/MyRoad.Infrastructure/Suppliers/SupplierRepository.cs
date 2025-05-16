using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Suppliers;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Suppliers
{
    public class SupplierRepository(
        AppDbContext dbContext,
        ISieveProcessor sieveProcessor
        ) : ISupplierRepository
    {
        public async Task<bool> CreateAsync(Supplier supplier)
        {
            await dbContext.Suppliers.AddAsync(supplier);
            return await dbContext.SaveChangesAsync()>0;
        }

        public async Task<Supplier?> FindByEmail(string? supplierEmail)
        {
            if (string.IsNullOrWhiteSpace(supplierEmail))
                return null;

            return await dbContext.Suppliers
                .FirstOrDefaultAsync(c => c.Email != null && c.Email.ToLower() == supplierEmail.ToLower());
        }

        public async Task<Supplier?> FindByPhoneNumber(string? supplierPhoneNumber)
        {
            var result = await dbContext.Suppliers.FirstOrDefaultAsync(x => x.PhoneNumber == supplierPhoneNumber);
            return result;
        }

        public async Task<ErrorOr<PaginatedResponse<Supplier>>> GetAsync(SieveModel sieveModel)
        {
            var query = dbContext.Suppliers.AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<Supplier>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<Supplier?> GetByIdAsync(long supplierId)
        {
            var supplier = await dbContext.Suppliers.FindAsync(supplierId);
            return supplier;
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            dbContext.Suppliers.Update(supplier);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
