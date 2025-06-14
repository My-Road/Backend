using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Payments.EmployeePayments;

public interface IEmployeePaymentRepository
{
    Task<bool> CreateAsync(EmployeePayment employeePayment);

    Task<bool>UpdateAsync(EmployeePayment employeePayment);

    Task<EmployeePayment?> GetByIdAsync(long employeePaymentId);

    Task<PaginatedResponse<EmployeePayment>> GetAsync(SieveModel sieveModel);

    Task<PaginatedResponse<EmployeePayment>> GetByEmployeeIdAsync(long employeeId, SieveModel sieveModel);

    Task<decimal> GetTotalPaymentAsync(DateOnly? from = null);
}