using ErrorOr;
using FluentValidation;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Suppliers;
using Sieve.Models;

namespace MyRoad.Domain.Payments.SupplierPayments
{
    public class SupplierPaymentService(
        ISupplierPaymentRepository supplierPaymentRepository,
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
        : ISupplierPaymentService
    {
        private readonly PaymentValidator _paymentValidator = new();
        private readonly SupplierPaymentValidator _supplierPayments = new();

        public async Task<ErrorOr<Success>> CreateAsync(SupplierPayment supplierPayment)
        {
            var validators = new List<IValidator<SupplierPayment>>
            {
                _paymentValidator,
                _supplierPayments
            };

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(supplierPayment);
                if (!result.IsValid)
                {
                    return result.ExtractErrors();
                }
            }

            var supplier = await supplierRepository.GetByIdAsync(supplierPayment.SupplierId);
            if (supplier is null || supplier.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            if (supplier.TotalPaidAmount >= supplier.TotalDueAmount)
            {
                return PaymentErrors.NoDueAmountLeft;
            }

            if (supplier.TotalPaidAmount + supplierPayment.Amount > supplier.TotalDueAmount)
            {
                return PaymentErrors.PaymentExceedsDue;
            }

            supplier.TotalPaidAmount += supplierPayment.Amount;
            try
            {
                await unitOfWork.BeginTransactionAsync();
                var updateResult = await supplierRepository.UpdateAsync(supplier);
                if (!updateResult)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    throw new Exception("Internal Server Error");
                }

                if (await supplierPaymentRepository.CreateAsync(supplierPayment))
                {
                    await unitOfWork.CommitTransactionAsync();
                    return new Success();
                }
            }
            catch 
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return new Success();

        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var payment = await supplierPaymentRepository.GetByIdAsync(id);

            if (payment is null || payment.IsDeleted)
            {
                return PaymentErrors.NotFound;
            }

            var supplier = await supplierRepository.GetByIdAsync(payment.SupplierId);
            if (supplier is null)
            {
                return SupplierErrors.NotFound;
            }

            supplier.TotalPaidAmount -= payment.Amount;

            var result = payment.Delete();

            if (result.IsError)
            {
                return result.Errors;
            }

            await supplierRepository.UpdateAsync(supplier);

            return new Success();

        }

        public async Task<ErrorOr<PaginatedResponse<SupplierPayment>>> GetAsync(SieveModel sieveModel)
        {
            var result = await supplierPaymentRepository.GetAsync(sieveModel);

            return result;
        }

        public async Task<ErrorOr<SupplierPayment>> GetByIdAsync(long id)
        {
            var payment = await supplierPaymentRepository.GetByIdAsync(id);
            if (payment is null)
            {
                return PaymentErrors.NotFound;
            }

            return payment;
        }

        public async Task<ErrorOr<PaginatedResponse<SupplierPayment>>> GetBySupplierIdAsync(long supplierId, SieveModel sieveModel)
        {
            var result = await supplierPaymentRepository.GetBySupplierIdAsync(supplierId, sieveModel);
            return result;
        }

        public async Task<ErrorOr<Success>> UpdateAsync(SupplierPayment supplierPayment)
        {
            var validators = new List<IValidator<SupplierPayment>>
            {
                _paymentValidator,
                _supplierPayments
            };

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(supplierPayment);
                if (!result.IsValid)
                {
                    return result.ExtractErrors();
                }
            }

            var existingPayment = await supplierPaymentRepository.GetByIdAsync(supplierPayment.Id);
            if (existingPayment is null || existingPayment.IsDeleted)
            {
                return PaymentErrors.NotFound;
            }

            var supplier = await supplierRepository.GetByIdAsync(supplierPayment.SupplierId);
            if (supplier is null || supplier.IsDeleted)
            {
                return SupplierErrors.NotFound;
            }

            try
            {
                await unitOfWork.BeginTransactionAsync();
                var newTotalPaidAmount = supplier.TotalPaidAmount - existingPayment.Amount + supplierPayment.Amount;
                if (newTotalPaidAmount > supplier.TotalDueAmount)
                {
                    return PaymentErrors.UpdatedPaymentExceedsDue;
                }

                supplier.TotalPaidAmount = newTotalPaidAmount;

                existingPayment.MapUpdatedSupplierPayment(supplierPayment);

                await supplierRepository.UpdateAsync(supplier);
                await supplierPaymentRepository.UpdateAsync(existingPayment);

                await unitOfWork.CommitTransactionAsync();

                return new Success();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}





