using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Reports;
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

        public async Task<List<EmployeeLog>> GetEmployeesLogForReportAsync(ReportFilter filter)
        {
            var query = dbContext.EmployeeLogs
                .Include(o => o.Employee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(o => o.Employee.FullName.Contains(filter.FullName));

            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(o => o.Employee.Address.Contains(filter.Address));

            if (filter.StartDate.HasValue)
                query = query.Where(o => o.Employee.StartDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(o => o.Employee.EndDate <= filter.EndDate.Value);

            return await query
                .OrderByDescending(o => o.Date)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}