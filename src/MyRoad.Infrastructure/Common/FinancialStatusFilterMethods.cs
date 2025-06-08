using MyRoad.Domain.Customers;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Suppliers;
using Sieve.Services;

namespace MyRoad.Infrastructure.Common;

public class FinancialStatusFilterMethods : ISieveCustomFilterMethods
{
    public IQueryable<Customer> RemainingAmount(IQueryable<Customer> source, string op, string[] values)
    {
        if (!decimal.TryParse(values[0], out decimal targetValue))
            return source;

        return op switch
        {
            "==" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) == targetValue),
            "!=" => source.Where(o =>(o.TotalDueAmount - o.TotalPaidAmount) != targetValue),
            ">"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) > targetValue),
            ">=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) >= targetValue),
            "<"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) < targetValue),
            "<=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) <= targetValue),
            _    => source
        };
    }

    public IQueryable<Employee> FilterByRemainingAmount(IQueryable<Employee> source, string op, string[] values)
    {
        if (!decimal.TryParse(values[0], out var targetValue))
            return source;

        return op switch
        {
            "==" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) == targetValue),
            "!=" => source.Where(o =>(o.TotalDueAmount - o.TotalPaidAmount) != targetValue),
            ">"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) > targetValue),
            ">=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) >= targetValue),
            "<"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) < targetValue),
            "<=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) <= targetValue),
            _    => source
        };
    }

    public IQueryable<Supplier> FilterByRemainingAmount(IQueryable<Supplier> source, string op, string[] values)
    {
        if (!decimal.TryParse(values[0], out var targetValue))
            return source;

        return op switch
        {
            "==" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) == targetValue),
            "!=" => source.Where(o =>(o.TotalDueAmount - o.TotalPaidAmount) != targetValue),
            ">"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) > targetValue),
            ">=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) >= targetValue),
            "<"  => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) < targetValue),
            "<=" => source.Where(o => (o.TotalDueAmount - o.TotalPaidAmount) <= targetValue),
            _    => source
        };
    }
}
