using Common.DataAccess.Models;

namespace Common.DataAccess.Interfaces;

/// <summary>
/// Интерфейс репозитория для заказов
/// </summary>
public interface IOrderRepository : IGenericRepository<Order>
{
    /// <summary>
    /// Создание и добавление заказа
    /// </summary>
    /// <param name="orderDate">Дата и время заказа</param>
    /// <param name="storageUntil">Дата и время до которого хранится заказ</param>
    /// <param name="status">Статус заказа</param>
    /// <param name="customerName">Имя покупателя</param>
    /// <param name="PhoneNumber">Номер телефона покупателя</param>
    /// <returns>Созданный заказ</returns>
    public Order CreateAndAdd(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber);

    /// <summary>
    /// Обновление заказа
    /// </summary>
    /// <param name="order">Заказ</param>
    /// <param name="orderDate">Дата и время заказа</param>
    /// <param name="storageUntil">Дата и время до которого хранится заказ</param>
    /// <param name="status">Статус заказа</param>
    /// <param name="customerName">Имя покупателя</param>
    /// <param name="phoneNumber">Номер телефона покупателя</param>
    /// <param name="items">Список продуктов в заказе</param>
    public void Update(Order order, DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? phoneNumber, ICollection<OrderItem> items);
}
