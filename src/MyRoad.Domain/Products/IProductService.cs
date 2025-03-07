namespace MyRoad.Domain.Products;

public interface IProductService
{
    void Create(Product product);

    Product GetById(int id);
}