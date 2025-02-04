using Common.DataAccess.Models;

namespace ProductService.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса для получения информации о заказах
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Асинхронное получение заказа
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>Заказ</returns>
    Task<Order> GetAsync(int id);

    /// <summary>
    /// Полуение всех заказов
    /// </summary>
    /// <returns>Список заказов</returns>
    Order[] GetAll();
}