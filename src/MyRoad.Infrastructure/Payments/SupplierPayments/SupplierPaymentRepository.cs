using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Payments.SupplierPayments
{
    internal class SupplierPaymentRepository(
        AppDbContext dbContext,
        ISieveProcessor sieveProcessor
    ) : ISupplierPaymentRepository
    {
        public async Task<bool> CreateAsync(SupplierPayment supplierPayment)
        {
            await dbContext.SupplierPayments.AddAsync(supplierPayment);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResponse<SupplierPayment>> GetAsync(SieveModel sieveModel)
        {
            var query = dbContext.SupplierPayments
                .Include(o => o.Supplier)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<SupplierPayment>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<SupplierPayment?> GetByIdAsync(long supplierPaymentId)
        {
            var payment = await dbContext.SupplierPayments
                .Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Id == supplierPaymentId && !x.IsDeleted);
            return payment;
        }

        public async Task<PaginatedResponse<SupplierPayment>> GetBySupplierIdAsync(long supplierId,
            SieveModel sieveModel)
        {
            var query = dbContext.SupplierPayments
                .Where(p => p.SupplierId == supplierId && !p.IsDeleted)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<SupplierPayment>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<decimal> GetTotalPaymentAsync(DateOnly? from = null)
        {
            from ??= DateOnly.FromDateTime(DateTime.Now.AddDays(-30));

            var result = await dbContext.SupplierPayments
                .Where(p => !p.IsDeleted && p.PaymentDate >= from)
                .SumAsync(p => p.Amount);

            return result;
        }

        public async Task<bool> UpdateAsync(SupplierPayment supplierPayment)
        {
            dbContext.SupplierPayments.Update(supplierPayment);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}