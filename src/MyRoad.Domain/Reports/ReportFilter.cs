namespace MyRoad.Domain.Reports
{
    public class ReportFilter
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
