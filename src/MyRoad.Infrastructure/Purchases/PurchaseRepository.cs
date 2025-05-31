using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Purchases;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Purchases
{
    public class PurchaseRepository(
        AppDbContext dbContext,
        ISieveProcessor sieveProcessor
    ) : IPurchaseRepository
    {
        public async Task<bool> CreateAsync(Purchase purchase)
        {
            await dbContext.Purchases.AddAsync(purchase);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResponse<Purchase>> GetAsync(SieveModel sieveModel)
        {
            var query = dbContext.Purchases
                .Include(s => s.Supplier)
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<Purchase>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<Purchase?> GetByIdAsync(long id)
        {
            var purchase = await dbContext.Purchases
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            return purchase;
        }

        public async Task<PaginatedResponse<Purchase>> GetBySupplierAsync(long supplierId, SieveModel sieveModel)
        {
            var query = dbContext.Purchases
                .Where(p => p.SupplierId == supplierId && !p.IsDeleted)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<Purchase>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<bool> UpdateAsync(Purchase purchase)
        {
            dbContext.Purchases.Update(purchase);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}