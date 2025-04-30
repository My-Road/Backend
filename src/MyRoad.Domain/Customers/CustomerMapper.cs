namespace MyRoad.Domain.Customers;

public static class CustomerMapper
{
    public static void MapUpdatedCustomer(this Customer existingCustomer, Customer updatedCustomer)
    {
        existingCustomer.FullName = updatedCustomer.FullName;
        existingCustomer.Email = updatedCustomer.Email;
        existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
        existingCustomer.Address = updatedCustomer.Address;
    }
}