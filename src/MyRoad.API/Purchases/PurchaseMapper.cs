using MyRoad.API.Purchases.RequestDto;
using MyRoad.Domain.Purchases;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Purchases
{
    [Mapper]
    public static partial class PurchaseMapper
    {
        public static partial Purchase ToDomainPurchase(this CreatePurchasesDto dto);
        public static partial Purchase ToDomainPurchase(this UpdatePurchasesDto dto);
    }
}
