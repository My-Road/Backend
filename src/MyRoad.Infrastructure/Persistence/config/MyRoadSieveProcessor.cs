using Microsoft.Extensions.Options;
using MyRoad.Infrastructure.Customers;
using MyRoad.Infrastructure.Employees;
using MyRoad.Infrastructure.Orders;
using MyRoad.Infrastructure.Payments.EmployeePayments;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Persistence.config;

public class MyRoadSieveProcessor(IOptions<SieveOptions> options) : SieveProcessor(options)
{
    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        return mapper
            .ApplyConfiguration<OrderSieveConfiguration>()
            .ApplyConfiguration<CustomerSieveConfiguration>()
            .ApplyConfiguration<EmployeePaymentSieveConfiguration>()
            .ApplyConfiguration<EmployeeSieveConfiguration>();
    }
}