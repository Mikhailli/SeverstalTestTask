using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

/// <summary>
/// Товары в заказе
/// </summary>
public class OrderItem : Entity
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;


    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Заказ
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
}
