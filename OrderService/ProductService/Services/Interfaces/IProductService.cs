using Common.DataAccess.Models;

namespace ProductService.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса для получения информации о товарах
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Асинхронное получение товара
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns>Товар</returns>
    Task<Product> GetAsync(int id);

    /// <summary>
    /// Получение всех товаров
    /// </summary>
    /// <returns>Список товаров</returns>
    Product[] GetAll();
}
