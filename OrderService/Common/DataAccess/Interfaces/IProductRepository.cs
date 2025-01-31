using Common.DataAccess.Models;

namespace Common.DataAccess.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Product? CreateAndAdd(string article, string name, string? description, decimal price, int stockQuantity);

    void Update(Product product, string article, string name, string? description, decimal price, int stockQuantity);
}
