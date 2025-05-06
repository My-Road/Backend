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
    public static Error CreationFailed => Error.Failure(
          code: "Employee.CreationFailed",
          description: "Something went wrong. Please verify the information you entered.");

    public static Error PhoneNumberAlreadyExists => Error.Conflict(
        code: "Employee.PhoneNumberExists",
        description: "Employee with phone number already exists"
    );

    public static Error CannotRemoveEmployeeLog => Error.Conflict(
     code: "Employee.CannotRemoveEmployeeLog",
     description:
     "Cannot remove this employee log because it would result in an overpayment — the paid amount would exceed the due amount."
     );
}