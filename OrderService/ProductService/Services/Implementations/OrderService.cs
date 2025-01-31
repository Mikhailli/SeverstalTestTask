using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using ProductService.Services.Interfaces;

namespace ProductService.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

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
