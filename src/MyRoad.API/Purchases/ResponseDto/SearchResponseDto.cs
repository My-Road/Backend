using MyRoad.API.Suppliers.ResponseDto;

namespace MyRoad.API.Purchases.ResponseDto;

public class SearchResponseDto : PurchaseResponseDto
{
    public SupplierDto  Supplier{ get; set; }
}