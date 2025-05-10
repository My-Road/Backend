using ErrorOr;

namespace MyRoad.Domain.EmployeesLogs
{
    public static class EmployeeLogErrors
    {
        public static Error NotFound => Error.NotFound(
        code: "EmployeeLog.NotFound",
        description: "can't find this EmployeeLog."

        );
        public static Error AlreadyDeleted => Error.NotFound(
        code: "EmployeeLog.AlreadyDeleted",
        description: "The EmployeeLog has already been deleted."
        );
        public static Error NotDeleted => Error.NotFound(
        code: "EmployeeLog.NotDeleted",
        description: "EmployeeLog is already active."
        );

        public static Error InvalidWageUpdate => Error.Conflict(
        code: "EmployeeLog.InvalidWageUpdate",
        description: "The update would cause the employee's total paid amount to exceed the total due amount."
        );

    }
}
