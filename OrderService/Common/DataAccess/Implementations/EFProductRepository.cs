using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;

namespace Common.DataAccess.Implementations;

/// <summary>
/// EF репозиторий для товаров
/// </summary>
/// <param name="context">Контекс базы данных</param>
internal class EFProductRepository(OrderDbContext context) : EFGenericRepository<Product>(context), IProductRepository
{
    public Product? CreateAndAdd(string article, string name, string? description, decimal price, int stockQuantity)
    {
        if (GetCount(product => product.Article == article && product.Name == name) > 0)
        {
            return null;
        }

        var product = new Product
        {
            Article = article,
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stockQuantity
        };

        Add(product);

        return product;
    }

    public void Update(Product product, string article, string name, string? description, decimal price, int stockQuantity)
    {
        product.Article = article;
        product.Name = name;
        product.Description = description;
        product.Price = price;
        product.StockQuantity = stockQuantity;

        Update(product);
    }
}
