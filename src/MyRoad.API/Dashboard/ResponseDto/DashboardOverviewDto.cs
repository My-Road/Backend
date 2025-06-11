namespace MyRoad.API.Dashboard.ResponseDto;

public class DashboardOverviewDto
{
    public long CustomerCount { get; set; }
    public long SupplierCount { get; set; }
    public long EmployeeCount { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    
    public decimal TotalIncomePaid { get; set; }      
    public decimal TotalExpensePaid { get; set; }
    public decimal Profit => TotalIncomePaid - TotalExpensePaid;
}