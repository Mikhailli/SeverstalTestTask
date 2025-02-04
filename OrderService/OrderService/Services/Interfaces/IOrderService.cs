using Common.DataAccess.Models;
using OrderService.ApiRequestModels;

namespace OrderService.Services.Interfaces;

/// <summary>
/// Интерфейс для сервиса изменения заказов
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Добавление заказ
    /// </summary>
    /// <param name="orderDate">Дата и время создания заказа</param>
    /// <param name="storageUntil">Дата и время до которого хранится заказ</param>
    /// <param name="status">Статус заказа</param>
    /// <param name="customerName">Имя покупателя</param>
    /// <param name="PhoneNumber">Номер телефона покупателя</param>
    /// <returns>Добавленный заказ</returns>
    Order AddOrder(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber);

    /// <summary>
    /// Удаление заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа для удаления</param>
    void DeleteOrder(int id);

    /// <summary>
    /// Изменнеие статуса заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа для изменения статуса</param>
    /// <param name="status">Новый статус</param>
    void ChangeStatus(int id, string status);

    /// <summary>
    /// Изменение товар в заказе
    /// </summary>
    /// <param name="id">Идентификатор заказа для изменения товаров</param>
    /// <param name="orderItemParameters">Список товаров для заказа</param>
    void ChangeOrderItems(int id, ICollection<OrderItemParameters> orderItemParameters);
}