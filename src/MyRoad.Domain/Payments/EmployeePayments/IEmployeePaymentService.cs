using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Payments.EmployeePayments;

public interface IEmployeePaymentService
{
    Task<ErrorOr<Success>> CreateAsync(EmployeePayment employeePayment);

    Task<ErrorOr<Success>> UpdateAsync(EmployeePayment employeePayment);

    Task<ErrorOr<Success>> DeleteAsync(long id, string note);

    Task<ErrorOr<EmployeePayment>> GetByIdAsync(long id);

    Task<ErrorOr<PaginatedResponse<EmployeePayment>>> GetByEmployeeIdAsync(long employeeId,SieveModel sieveModel);

    Task<ErrorOr<PaginatedResponse<EmployeePayment>>> GetAsync(SieveModel sieveModel);
}