using MyRoad.Domain.Products;

namespace MyRoad.Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = [
        new Product
        {
            Id = 1,
            Name = "GG",
            Price = 100
        }
    ];
    
    public void Create(Product product)
    {
        _products.Add(product);
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
}