using MyRoad.Domain.Common;

namespace MyRoad.Domain.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public void Create(Product product)
    {
        // Validation logic
        
        productRepository.Create(product);
    }

    public Product GetById(int id)
    {
        var product = productRepository.GetById(id);

        if (product is null)
        {
            throw new NotFoundException(id.ToString());
        }

        return product;
    }
}