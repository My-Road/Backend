namespace MyRoad.Domain.Suppliers
{
    public static class SupplierMapper
    {
        public static void MapUpdatedSupplier(this Supplier existingSupplier, Supplier updatedSupplier)
        {
            existingSupplier.FullName = updatedSupplier.FullName;
            existingSupplier.Email = updatedSupplier.Email;
            existingSupplier.PhoneNumber = updatedSupplier.PhoneNumber;
            existingSupplier.Address = updatedSupplier.Address;
        }
    }
}
