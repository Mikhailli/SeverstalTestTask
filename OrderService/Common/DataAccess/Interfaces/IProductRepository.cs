using Common.DataAccess.Models;

namespace Common.DataAccess.Interfaces;

/// <summary>
/// Интерфейс репозитория для товара
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>
    /// Создание и добавление товара
    /// </summary>
    /// <param name="article">Артикул товара</param>
    /// <param name="name">Наименование товара</param>
    /// <param name="description">Описание товара</param>
    /// <param name="price">Стоимость товара</param>
    /// <param name="stockQuantity">Количество товара на складе</param>
    /// <returns>Созданный товар</returns>
    Product? CreateAndAdd(string article, string name, string? description, decimal price, int stockQuantity);

    /// <summary>
    /// Обновление товара
    /// </summary>
    /// <param name="product">Товар</param>
    /// <param name="article">Артикул товара</param>
    /// <param name="name">Наименование товара</param>
    /// <param name="description">Описание товара</param>
    /// <param name="price">Стоимость товара</param>
    /// <param name="stockQuantity">Количество товара на складе</param>
    void Update(Product product, string article, string name, string? description, decimal price, int stockQuantity);
}
