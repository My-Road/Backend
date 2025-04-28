using ErrorOr;

namespace MyRoad.Domain.Employees;

public static class EmployeeErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Employee.NotFound",
        description: "Not Found Employee");
    public static Error NotDeleted => Error.Validation(
        code: "Employee.NotDeleted",
        description: "Employee is already active."
    );
    public static Error AlreadyDeleted => Error.Validation(
         code: "Employee.AlreadyDeleted",
         description: "Employee is already deleted."
     );
}