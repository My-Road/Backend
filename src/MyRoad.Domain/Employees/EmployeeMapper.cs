namespace MyRoad.Domain.Employees
{
    public static class EmployeeMapper
    {
        public static void MapUpdatedEmployee(this Employee existingEmployee, Employee updatedEmployee)
        {
            existingEmployee.FullName = updatedEmployee.FullName;
            existingEmployee.JobTitle = updatedEmployee.JobTitle;
            existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
            existingEmployee.Address = updatedEmployee.Address;
            existingEmployee.Notes = updatedEmployee.Notes;
            existingEmployee.Status = updatedEmployee.Status;
            existingEmployee.StartDate = updatedEmployee.StartDate;
            existingEmployee.EndDate = updatedEmployee.EndDate;
        }
    }
}
