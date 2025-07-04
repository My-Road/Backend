﻿using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.EmployeesLogs
{
    public class EmployeeLogRepository(
        AppDbContext dbContext,
        ISieveProcessor sieveProcessor
    ) : IEmployeeLogRepository
    {
        public async Task<bool> CreateAsync(EmployeeLog employeelog)
        {
            await dbContext.EmployeeLogs.AddAsync(employeelog);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedResponse<EmployeeLog>> GetAsync(SieveModel sieveModel)
        {
            var query = dbContext.EmployeeLogs
                .Include(log => log.Employee)
                .Where(x => !x.IsDeleted && x.Employee.IsActive)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<EmployeeLog>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }


        public async Task<IEnumerable<EmployeeLog>> GetLogsByDateAsync(long employeeId, DateOnly date)
        {
            return await dbContext.EmployeeLogs
                .Where(log => !log.IsDeleted && log.EmployeeId == employeeId && log.Date == date)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalExpensesAsync(DateOnly? from = null)
        {
            from ??= DateOnly.FromDateTime(DateTime.Now.AddDays(-30));

            var employeeLogs = await dbContext.EmployeeLogs
                .Where(logs => !logs.IsDeleted && logs.Date >= from)
                .ToListAsync();

            return employeeLogs.Sum(l => l.DailyWage);
        }

        public async Task<PaginatedResponse<EmployeeLog>> GetByEmployeeAsync(long employeeId, SieveModel sieveModel)
        {
            var query = dbContext.EmployeeLogs
                .Where(p => p.EmployeeId == employeeId && !p.IsDeleted)
                .AsQueryable();

            var totalItems = await sieveProcessor
                .Apply(sieveModel, query, applyPagination: false)
                .CountAsync();

            query = sieveProcessor.Apply(sieveModel, query);

            var result = await query.AsNoTracking().ToListAsync();

            return new PaginatedResponse<EmployeeLog>
            {
                Items = result,
                TotalCount = totalItems,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10,
            };
        }

        public async Task<EmployeeLog?> GetByIdAsync(long id)
        {
            return await dbContext.EmployeeLogs
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<bool> UpdateAsync(EmployeeLog employeelog)
        {
            dbContext.EmployeeLogs.Update(employeelog);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<EmployeeLog>> GetEmployeesLogForReportAsync(SieveModel sieveModel)
        {
            var query = dbContext.EmployeeLogs
                .Include(o => o.Employee)
                .Where(o => !o.IsDeleted && o.Employee.IsActive)
                .AsQueryable();

            query = sieveProcessor.Apply(sieveModel, query, applyPagination: false);

            query = query.OrderByDescending(o => o.Date);

            return await query.AsNoTracking().ToListAsync();
        }

    }
}