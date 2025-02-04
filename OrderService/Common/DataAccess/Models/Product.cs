using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

/// <summary>
/// Модель для товара
/// </summary>
public class Product : Entity
{
    /// <summary>
    /// Артикул товара
    /// </summary>
    public string Article { get; set; } = null!;

    /// <summary>
    /// Наименование товара
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание товара
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Стоимость товара
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество товара на складе
    /// </summary>
    public int StockQuantity { get; set; }
}
