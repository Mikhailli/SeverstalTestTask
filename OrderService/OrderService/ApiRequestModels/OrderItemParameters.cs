namespace OrderService.ApiRequestModels;

/// <summary>
/// API модель для передачи товаров в заказе
/// </summary>
public class OrderItemParameters
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
}
