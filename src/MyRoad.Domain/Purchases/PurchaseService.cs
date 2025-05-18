using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Suppliers;
using MyRoad.Domain.Users;
using Sieve.Models;

namespace MyRoad.Domain.Purchases
{
    public class PurchaseService(
        IUserContext userContext,
        IPurchaseRepository purchaseRepository,
        ISupplierRepository supplierRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IPurchaseService
    {
        private readonly PurchaseVaildator _purchaseVaildator = new();

        public async Task<ErrorOr<Success>> CreateAsync(Purchase purchase)
        {
            var result = await _purchaseVaildator.ValidateAsync(purchase);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }

            var supplier = await supplierRepository.GetByIdAsync(purchase.SupplierId);
            if (supplier is null || supplier.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            var user = await userRepository.GetByIdAsync(purchase.CreatedByUserId);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            await unitOfWork.BeginTransactionAsync();
            try
            {
                switch (user.Role)
                {
                    case UserRole.Admin when userContext.Role == UserRole.Admin:
                    case UserRole.Manager when userContext.Role == UserRole.Manager:
                        supplier.TotalDueAmount += purchase.TotalDueAmount;
                        purchase.IsCompleted = true;
                        break;

                    default:
                        return UserErrors.UnauthorizedUser;
                }

                await purchaseRepository.CreateAsync(purchase);
                await supplierRepository.UpdateAsync(supplier);
                await unitOfWork.CommitTransactionAsync();

                return new Success();
            }
            catch 
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var purchase = await purchaseRepository.GetByIdAsync(id);
            if (purchase is null || purchase.IsDeleted)
            {
                return PurchaseErrors.NotFound;
            }

            var supplier = await supplierRepository.GetByIdAsync(purchase.SupplierId);
            if (supplier is null)
            {
                return SupplierErrors.NotFound;
            }

            if (supplier.RemainingAmount == 0 || supplier.TotalDueAmount - purchase.TotalDueAmount < supplier.TotalPaidAmount)
            {
                return SupplierErrors.CannotUpdatePurchase;
            }

            var result = purchase.Delete();
            if (result.IsError)
            {
                return result.Errors;
            }

            supplier.TotalDueAmount -= purchase.TotalDueAmount;
            await purchaseRepository.UpdateAsync(purchase);
            await supplierRepository.UpdateAsync(supplier);

            return new Success();

        }

        public async Task<ErrorOr<PaginatedResponse<Purchase>>> GetAsync(SieveModel sieveModel)
        {
            var result = await purchaseRepository.GetAsync(sieveModel);

            return result;
        }

        public async Task<ErrorOr<Purchase>> GetByIdAsync(long id)
        {
            var purchase = await purchaseRepository.GetByIdAsync(id);
            if (purchase is null ||purchase.IsDeleted)
            {
                return PurchaseErrors.NotFound;
            }

            return purchase;
        }

        public async Task<ErrorOr<PaginatedResponse<Purchase>>> GetBySupplierIdAsync(long supplierId, SieveModel sieveModel)
        {
            var result = await purchaseRepository.GetBySupplierAsync(supplierId, sieveModel);
            return result;
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Purchase purchase)
        {
            var validationResult = await _purchaseVaildator.ValidateAsync(purchase);
            if (!validationResult.IsValid)
            {
                return validationResult.ExtractErrors();
            }

            var existingPurchase = await purchaseRepository.GetByIdAsync(purchase.Id);
            if (existingPurchase is null || existingPurchase.IsDeleted)
            {
                return PurchaseErrors.NotFound;
            }

            var supplier = await supplierRepository.GetByIdAsync(purchase.SupplierId);
            if (supplier is null || supplier.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            if (purchase.Price <= 0)
            {
                return PurchaseErrors.InvalidPrice;
            }

            var newTotalDueAmount = supplier.TotalDueAmount - existingPurchase.TotalDueAmount + purchase.TotalDueAmount;
            if (supplier.TotalPaidAmount > newTotalDueAmount)
            {
                return SupplierErrors.CannotUpdatePurchase;
            }

            try
            {
                await unitOfWork.BeginTransactionAsync();

                supplier.TotalDueAmount = newTotalDueAmount;
                existingPurchase.MapUpdatedPurchase(purchase);

                await supplierRepository.UpdateAsync(supplier);
                await purchaseRepository.UpdateAsync(existingPurchase);
                await unitOfWork.CommitTransactionAsync();

                return new Success();
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}


