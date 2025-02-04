using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

/// <summary>
/// Модель для заказа
/// </summary>
public class Order : Entity
{
    /// <summary>
    /// Дата и время создания заказа
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Дата и время до которого хранится заказ
    /// </summary>
    public DateTime StorageUntil { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Имя покупателя
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Телефон покупателя
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Коллекция товаров, входящих в заказ
    /// </summary>
    public virtual ICollection<OrderItem> Items { get; set; } = null!;
}
