namespace MyRoad.Domain.Purchases
{
    public static class PurchaseMapper
    {
        public static void MapUpdatedPurchase(this Purchase existingPurchase , Purchase updatedPurchase)
        {
            existingPurchase.PurchasesDate = updatedPurchase.PurchasesDate;
            existingPurchase.GoodsDeliverer = updatedPurchase.GoodsDeliverer;
            existingPurchase.GoodsDelivererPhoneNumber = updatedPurchase.GoodsDelivererPhoneNumber;
            existingPurchase.Price = updatedPurchase.Price;
            existingPurchase.Quantity = updatedPurchase.Quantity;
            existingPurchase.CreatedByUserId = updatedPurchase.CreatedByUserId;
            existingPurchase.SupplierId = updatedPurchase.SupplierId;
            existingPurchase.Notes = updatedPurchase.Notes;
        }
       
    }
}
