using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Products.Requests;
using MyRoad.API.Products.Responses;
using MyRoad.Domain.Products;

namespace MyRoad.API.Products;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateProductRequest product)
    {
        var productEntity = new Product
        {
            Name = product.Name,
            Price = product.Price
        };
        
        productService.Create(productEntity);
        
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = productService.GetById(id);
        
        return Ok(new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        });
    }
}