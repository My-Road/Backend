using MyRoad.API.Suppliers.RequestDto;
using MyRoad.API.Suppliers.ResponseDto;
using MyRoad.Domain.Suppliers;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Suppliers
{
    [Mapper]
    public static partial class SupplierMapper
    {
        public static partial Supplier ToDomainSupplier(this CreateSuppliersDto dto);
        public static partial Supplier ToDomainSupplier(this UpdateSuppliersDto dto);
        public static partial SupplierResponseDto ToSupplierResponseDto(this Supplier dto);
    }
}
