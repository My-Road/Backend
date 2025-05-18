using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Suppliers.RequestDto;
using MyRoad.Domain.Suppliers;

namespace MyRoad.API.Suppliers
{
    [Route("api/v{version:apiVersion}/supplier")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SupplierController (
        ISupplierService supplierService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSuppliersDto dto)
        {
            var response = await supplierService.CreateAsync(dto.ToDomainSupplier());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await supplierService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPut("restore/{id:long}")]
        public async Task<IActionResult> Restore(long id)
        {
            var result = await supplierService.RestoreAsync(id);
            return ResponseHandler.HandleResult(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await supplierService.GetAsync(request.ToSieveModel());

            return ResponseHandler.HandleResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSuppliersDto dto)
        {
            var response = await supplierService.UpdateAsync(dto.ToDomainSupplier());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await supplierService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }
    }
}
