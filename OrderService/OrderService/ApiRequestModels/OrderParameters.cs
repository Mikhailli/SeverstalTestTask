namespace OrderService.ApiRequestModels;

/// <summary>
/// API модель для заказа
/// </summary>
public class OrderParameters
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
    /// Номер телефона покупателя
    /// </summary>
    public string? PhoneNumber { get; set; }
}
