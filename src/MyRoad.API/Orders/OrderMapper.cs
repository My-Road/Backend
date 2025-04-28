using MyRoad.API.Orders.RequestDto;
using MyRoad.Domain.Orders;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Orders;

[Mapper]
public static partial class OrderMapper
{
    public static partial Order ToDomainOrder(this CreateOrderDto dto);
    public static partial Order ToDomainOrder(this UpdateOrderDto dto);
}