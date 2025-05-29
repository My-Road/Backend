using MyRoad.API.Suppliers.ResponseDto;

namespace MyRoad.API.Purchases.ResponseDto;

public class SearchResponseDto : SupplierResponseDto
{
    public SupplierDto  Supplier{ get; set; }
}