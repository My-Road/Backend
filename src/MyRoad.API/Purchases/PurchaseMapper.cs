using MyRoad.API.Orders.ResponseDto;
using MyRoad.API.Purchases.RequestDto;
using MyRoad.API.Purchases.ResponseDto;
using MyRoad.Domain.Purchases;
using Riok.Mapperly.Abstractions;
using SearchResponseDto = MyRoad.API.Purchases.ResponseDto.SearchResponseDto;

namespace MyRoad.API.Purchases
{
    [Mapper]
    public static partial class PurchaseMapper
    {
        public static partial Purchase ToDomainPurchase(this CreatePurchasesDto dto);
        public static partial Purchase ToDomainPurchase(this UpdatePurchasesDto dto);
        public static partial PurchaseResponseDto ToPurchaseResponseDto(this Purchase purchase);
        public static partial SearchResponseDto ToSearchResponseDto(this Purchase dto);
    }
}
