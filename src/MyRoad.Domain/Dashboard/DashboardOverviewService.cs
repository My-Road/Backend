using MyRoad.Domain.Customers;
using MyRoad.Domain.Employees;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Suppliers;
using ErrorOr;

namespace MyRoad.Domain.Dashboard;

public class DashboardOverviewService(
    ICustomerRepository customerRepository,
    IEmployeeRepository employeeRepository,
    ISupplierRepository supplierRepository,
    IOrderRepository orderRepository,
    IEmployeeLogRepository employeeLogRepository,
    IPurchaseRepository purchaseRepository,
    ICustomerPaymentRepository customerPaymentRepository,
    ISupplierPaymentRepository supplierPaymentRepository,
    IEmployeePaymentRepository employeePaymentRepository
    ) : IDashboardOverviewService
{
    public async Task<ErrorOr<DashboardOverview>> ExecuteAsync()
    {
        
        var result = new DashboardOverview  
        {
            CustomerCount = await customerRepository.CountAsync(),
            EmployeeCount = await employeeRepository.CountAsync(),
            SupplierCount = await supplierRepository.CountAsync(),
            TotalIncome = await orderRepository.GetTotalIncomeAsync(),
            TotalExpense = await employeeLogRepository.GetTotalExpensesAsync() + 
                           await purchaseRepository.GetTotalExpensesAsync(),
            TotalIncomePaid = await customerPaymentRepository.GetTotalPaymentAsync(),
            TotalExpensePaid = await employeePaymentRepository.GetTotalPaymentAsync() + 
                               await supplierPaymentRepository.GetTotalPaymentAsync(),
            

        };
        
        return result;
    }
}