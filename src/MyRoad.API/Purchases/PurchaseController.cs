using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Purchases.RequestDto;
using MyRoad.Domain.Purchases;

namespace MyRoad.API.Purchases
{
    [Route("api/v{version:apiVersion}/purchases")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PurchaseController(IPurchaseService purchaseService)
        : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePurchasesDto dto)
        {
            var response = await purchaseService.CreateAsync(dto.ToDomainPurchase());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await purchaseService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await purchaseService.GetAsync(request.ToSieveModel());

            return ResponseHandler.HandleResult(
                response.ToContractPaginatedList(PurchaseMapper.ToSearchResponseDto)
            );
        }

        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdatePurchasesDto dto)
        {
            var response = await purchaseService.UpdateAsync(dto.ToDomainPurchase());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await purchaseService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(
                response.ToContract(PurchaseMapper.ToPurchaseResponseDto)
            );
        }

        [HttpPost("by-supplier/{supplierId:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GetBySupplierId(long supplierId, [FromBody] RetrievalRequest request)
        {
            var response = await purchaseService.GetBySupplierIdAsync(supplierId, request.ToSieveModel());
            return ResponseHandler.HandleResult(
                response.ToContractPaginatedList(PurchaseMapper.ToPurchaseResponseDto)
            );
        }
    }
}