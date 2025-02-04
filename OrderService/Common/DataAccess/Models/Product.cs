using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

/// <summary>
/// Модель для продукта
/// </summary>
public class Product : Entity
{
    public string Article { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
