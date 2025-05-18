using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;
using Sieve.Models;

namespace MyRoad.Domain.Suppliers
{
    public class SupplierService(
        ISupplierRepository supplierRepository
    ) : ISupplierService
    {
        private readonly SupplierValidator _supplierValidator = new();

        public async Task<ErrorOr<Success>> CreateAsync(Supplier supplier)
        {
            var validate = await _supplierValidator.ValidateAsync(supplier);
            if (!validate.IsValid)
            {
                return validate.ExtractErrors();
            }

            var result = await supplierRepository.FindByPhoneNumber(supplier.PhoneNumber);

            if (result is not null && result.Id != supplier.Id)
            {
                return SupplierErrors.PhoneNumberAlreadyExists;
            }

            result = await supplierRepository.FindByEmail(supplier.Email);

            if (result is not null && result.Id != supplier.Id)
            {
                return SupplierErrors.EmailAlreadyExists;
            }
            supplier.TotalDueAmount = Math.Round(supplier.TotalDueAmount, 2);

            var isCreated = await supplierRepository.CreateAsync(supplier);

            return isCreated ? new Success() : new ErrorOr<Success>();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var supplier = await supplierRepository.GetByIdAsync(id);
            if (supplier is null)
            {
                return SupplierErrors.NotFound;
            }

            var result = supplier.Delete();
            if (result.IsError)
            {
                return result.Errors;
            }

            await supplierRepository.UpdateAsync(supplier);

            return new Success();
        }

        public async Task<ErrorOr<PaginatedResponse<Supplier>>> GetAsync(SieveModel sieveModel)
        {
            var result = await supplierRepository.GetAsync(sieveModel);
            return result;
        }

        public async Task<ErrorOr<Supplier>> GetByIdAsync(long id)
        {
            var supplier = await supplierRepository.GetByIdAsync(id);
            if (supplier is null || supplier.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            return supplier;
        }

        public async Task<ErrorOr<Success>> RestoreAsync(long id)
        {
            var supplier = await supplierRepository.GetByIdAsync(id);
            if (supplier is null)
            {
                return SupplierErrors.NotFound;
            }

            var result = supplier.Restore();
            if (result.IsError)
            {
                return result.Errors;
            }

            await supplierRepository.UpdateAsync(supplier);
            return new Success();
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Supplier supplier)
        {
            var validate = await _supplierValidator.ValidateAsync(supplier);
            if (!validate.IsValid)
            {
                return validate.ExtractErrors();
            }

            var result = await supplierRepository.FindByPhoneNumber(supplier.PhoneNumber);

            if (result is not null && result.Id != supplier.Id)
            {
                return SupplierErrors.PhoneNumberAlreadyExists;
            }

            result = await supplierRepository.FindByEmail(supplier.Email);

            if (result is not null && result.Id != supplier.Id)
            {
                return SupplierErrors.EmailAlreadyExists;
            }

            result = await supplierRepository.GetByIdAsync(supplier.Id);
            if (result is null || result.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            supplier.TotalDueAmount = Math.Round(supplier.TotalDueAmount, 2);
            result.MapUpdatedSupplier(supplier);
            await supplierRepository.UpdateAsync(result);
            return new Success();
        }
    }
}