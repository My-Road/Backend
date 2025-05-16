using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Purchases.RequestDto;
using MyRoad.Domain.Purchases;

namespace MyRoad.API.Purchases
{
    [Route("api/v{version:apiVersion}/purchase")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PurchaseController (IPurchaseService purchaseService)
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePurchasesDto dto)
        {
            var response = await purchaseService.CreateAsync(dto.ToDomainPurchase());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await purchaseService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await purchaseService.GetAsync(request.ToSieveModel());

            return ResponseHandler.HandleResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePurchasesDto dto)
        {
            var response = await purchaseService.UpdateAsync(dto.ToDomainPurchase());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await purchaseService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("by-purchase/{supplierId:long}")]
        public async Task<IActionResult> GetBySupplierId(long supplierId, [FromBody] RetrievalRequest request)
        {
            var response = await purchaseService.GetBySupplierIdAsync(supplierId, request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }
    }
}
