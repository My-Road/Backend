using MyRoad.API.Customers.ResponseDto;

namespace MyRoad.API.Orders.ResponseDto;

public class SearchResponseDto : OrderResponseDto
{
    public CustomerDto  Customer{ get; set; }
}