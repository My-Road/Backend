namespace MyRoad.Domain.Products;

public interface IProductRepository
{
    void Create(Product product);
    
    Product? GetById(int id);
}