using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using ProductService.Services.Interfaces;

namespace ProductService.Services.Implementations;

/// <summary>
/// Сервис для получения информации о заказах
/// </summary>
/// <param name="orderRepository">Репозиторий для заказов</param>
public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Order> GetAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            throw new Exception($"Заказ с идентификатором {id} не найден");
        }

        return order;
    }

    public Order[] GetAll()
    {
        return _orderRepository.GetAll().ToArray();
    }
}
