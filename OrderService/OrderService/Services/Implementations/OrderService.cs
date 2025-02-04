using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using OrderService.ApiRequestModels;
using OrderService.Services.Interfaces;

namespace OrderService.Services.Implementations;

public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Order AddOrder(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber)
    {
        var order = _orderRepository.CreateAndAdd(orderDate, storageUntil, status, customerName, PhoneNumber);

        _unitOfWork.Commit();

        return order;
    }

    public void ChangeStatus(int id, string status)
    {
        var orderToUpdate = _orderRepository.GetById(id);

        if (orderToUpdate is null)
        {
            throw new Exception($"Заказ с идентификатором {id} не найден");
        }

        _orderRepository.Update(orderToUpdate, orderToUpdate.OrderDate, orderToUpdate.StorageUntil, 
            status, orderToUpdate.CustomerName, orderToUpdate.PhoneNumber, orderToUpdate.Items);
        _unitOfWork.Commit();
    }

    public void DeleteOrder(int id)
    {
        var orderToDelete = _orderRepository.GetById(id);

        if (orderToDelete is null)
        {
            throw new Exception($"Заказ с идентификатором {id} не найден");
        }

        _orderRepository.Delete(orderToDelete);
        _unitOfWork.Commit();
    }

    public async void ChangeOrderItems(int id, ICollection<OrderItemParameters> orderItemParameters)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(id) 
            ?? throw new Exception($"Заказ с идентификатором {id} не найден");

        orderToUpdate.Items.Clear();
        foreach (var item in orderItemParameters)
        {
            if (await _productRepository.GetByIdAsync(item.ProductId) is null)
            {
                throw new Exception($"Товар с идентификатором {item.ProductId} не найден");
            }

            orderToUpdate.Items.Add(new OrderItem() { OrderId = id, ProductId = item.ProductId, Quantity = item.Quantity });
        }

        _orderRepository.Update(orderToUpdate, orderToUpdate.OrderDate, orderToUpdate.StorageUntil,
            orderToUpdate.Status, orderToUpdate.CustomerName, orderToUpdate.PhoneNumber, orderToUpdate.Items);
        _unitOfWork.Commit();
    }
}
