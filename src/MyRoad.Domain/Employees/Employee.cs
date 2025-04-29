using MyRoad.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using MyRoad.Domain.EmployeeLog;
using MyRoad.Domain.Payments.EmployeePayments;
using ErrorOr;

namespace MyRoad.Domain.Employees
{
    public class Employee : BaseEntity<long>
    {
        public string? FullName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }
        public string? Notes { get; set; }
        public decimal TotalDueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal RemainingAmount => TotalDueAmount - TotalPaidAmount;
        public bool IsDeleted { get; set; } = false;
        public ErrorOr<Success> Restore()
        {
            if (!IsDeleted)
            {
                return EmployeeErrors.NotDeleted;
            }

            IsDeleted = false;
            return new Success();
        }
        public ErrorOr<Success> Delete()
        {
            if (IsDeleted)
            {
                return EmployeeErrors.AlreadyDeleted;
            }

            IsDeleted = true;
            EndDate = DateTime.UtcNow;

            return new Success();
        }
        public ICollection<EmployeePayment> Payments { get; set; } = new List<EmployeePayment>();
        public ICollection<EmployeeLogs> Logs { get; set; } = new List<EmployeeLogs>();

    }
}