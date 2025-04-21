using ErrorOr;

namespace MyRoad.Domain.Employees;

public static class EmployeeErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Employee.NotFound",
        description: "Not Found Employee");
   
}