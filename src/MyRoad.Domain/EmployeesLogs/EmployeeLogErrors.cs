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

    }
}
