using MyRoad.Domain.Common.Entities;

namespace MyRoad.Domain.Employees
{
    public class Employee : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
