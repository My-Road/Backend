using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Reports;
using MyRoad.Domain.Suppliers;
using MyRoad.Domain.Users;
using Sieve.Models;

namespace MyRoad.Domain.Purchases
{
    public class PurchaseService(
        IPurchaseRepository purchaseRepository,
        ISupplierRepository supplierRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext) : IPurchaseService
    {
        private readonly PurchaseValidator _purchaseValidator = new();

        public async Task<ErrorOr<Success>> CreateAsync(Purchase purchase)
        {
            var result = await _purchaseValidator.ValidateAsync(purchase);
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
                    case UserRole.FactoryOwner when userContext.Role == UserRole.FactoryOwner:    
                        supplier.TotalDueAmount += purchase.TotalDueAmount;
                        purchase.IsCompleted = true;
                        break;
                    case UserRole.Manager when userContext.Role == UserRole.Manager:
                        purchase.IsCompleted = false;
                        purchase.Price = 0;
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

            if (supplier.RemainingAmount == 0 ||
                supplier.TotalDueAmount - purchase.TotalDueAmount < supplier.TotalPaidAmount)
            {
                return PurchaseErrors.CannotRemovePurchase;
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
            if (purchase is null || purchase.IsDeleted)
            {
                return PurchaseErrors.NotFound;
            }

            return purchase;
        }

        public async Task<ErrorOr<PaginatedResponse<Purchase>>> GetBySupplierIdAsync(long supplierId,
            SieveModel sieveModel)
        {
            var result = await purchaseRepository.GetBySupplierAsync(supplierId, sieveModel);
            return result;
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Purchase purchase)
        {
            var validationResult = await _purchaseValidator.ValidateAsync(purchase);
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
                return PurchaseErrors.CannotUpdatePurchase;
            }

            try
            {
                await unitOfWork.BeginTransactionAsync();

                supplier.TotalDueAmount = newTotalDueAmount;
                purchase.IsCompleted = true;
                
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

        public async Task<ErrorOr<List<Purchase>>> GetPurchasesForReportAsync(SieveModel sieveModel)
        {
            var purchase = await purchaseRepository.GetPurchaseForReportAsync(sieveModel);

            return purchase;
        }
    }
}