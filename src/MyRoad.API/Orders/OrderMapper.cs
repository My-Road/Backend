using MyRoad.API.EmployeesLogs.ResponseDto;
using MyRoad.API.Orders.RequestDto;
using MyRoad.API.Orders.ResponseDto;
using MyRoad.Domain.Orders;
using Riok.Mapperly.Abstractions;
using SearchResponseDto = MyRoad.API.Orders.ResponseDto.SearchResponseDto;

namespace MyRoad.API.Orders;

[Mapper]
public static partial class OrderMapper
{
    public static partial Order ToDomainOrder(this CreateOrderDto dto);
    public static partial Order ToDomainOrder(this UpdateOrderDto dto);

    public static partial OrderResponseDto ToDomainOrderResponseDto(Order dto);
    public static partial SearchResponseDto ToSearchResponseDto(Order dto);
}