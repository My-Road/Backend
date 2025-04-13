using MyRoad.Domain.Employees;

namespace MyRoad.Domain.Payments;

public class EmployeePayment : Payment
{
    public long EmployeeId { get; set; }
    public required Employee Employee { get; set; }
 
}