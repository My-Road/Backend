using MyRoad.Domain.Common.Entities;

namespace MyRoad.Domain.Workers
{
    public class Worker : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public decimal DailySalary { get; set; }
        public DateTime StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public WorkerStatus Status { get; set; }
        public string Notes { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
