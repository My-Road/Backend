using MyRoad.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Payments.EmployeePayments;

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
        [NotMapped] public decimal RemainingAmount => TotalDueAmount - TotalPaidAmount;

        public ICollection<EmployeePayment> Payments { get; set; } = new List<EmployeePayment>();
        public ICollection<EmployeeLog> Logs { get; set; } = new List<EmployeeLog>();

    }
}