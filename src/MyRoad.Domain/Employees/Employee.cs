using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Payments.EmployeePayments;
using ErrorOr;
using System.Text.Json.Serialization;

namespace MyRoad.Domain.Employees
{
    public class Employee : BaseEntity<long>
    {
        [JsonPropertyName("employeeName")] public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; } = true;
        public decimal TotalDueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal RemainingAmount => TotalDueAmount - TotalPaidAmount;

        public ErrorOr<Success> Restore()
        {
            if (Status)
            {
                return EmployeeErrors.NotDeleted;
            }

            Status = true;
            return new Success();
        }

        public ErrorOr<Success> Delete()
        {
            if (!Status)
            {
                return EmployeeErrors.AlreadyDeleted;
            }

            Status = false;
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            return new Success();
        }

        public ICollection<EmployeePayment> Payments { get; set; } = new List<EmployeePayment>();
        public ICollection<EmployeeLog> Logs { get; set; } = new List<EmployeeLog>();
    }
}