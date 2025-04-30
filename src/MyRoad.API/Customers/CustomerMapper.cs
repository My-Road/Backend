using MyRoad.API.Customers.RequestDto;
using MyRoad.Domain.Customers;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Customers;

[Mapper]
public static partial class CustomerMapper
{
    public static partial Customer ToDomainCustomer(this CreateCustomerDto dto);
    public static partial Customer ToDomainCustomer(this UpdateCustomerDto dto);
}