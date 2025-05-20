﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Payments.SupplierPayments.RequestDto;
using MyRoad.Domain.Payments.SupplierPayments;

namespace MyRoad.API.Payments.SupplierPayments
{
    [Route("api/v{version:apiVersion}/supplier-payments")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SupplierPaymentController(
        ISupplierPaymentService supplierPaymentService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSupplierPaymentDto dto)
        {
            var response = await supplierPaymentService.CreateAsync(dto.ToSupplierPayment());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await supplierPaymentService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await supplierPaymentService.GetAsync(request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSupplierPaymentDto dto)
        {
            var response = await supplierPaymentService.UpdateAsync(dto.ToSupplierPayment());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await supplierPaymentService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("supplier/{supplierId:long}")]
        public async Task<IActionResult> GetBySupplierId(long supplierId, [FromBody] RetrievalRequest request)
        {
            var response = await supplierPaymentService.GetBySupplierIdAsync(supplierId, request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }
    }
}
