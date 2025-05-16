namespace MyRoad.API.Suppliers.RequestDto
{
    public class UpdateSuppliersDto
    {
        public long Id { get; set; }
        public string? SupplierName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
