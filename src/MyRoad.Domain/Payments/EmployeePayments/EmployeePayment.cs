using MyRoad.Domain.Employees;

namespace MyRoad.Domain.Payments.EmployeePayments;

public class EmployeePayment : Payment
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
}