using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using ProductService.Services.Interfaces;

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

    public void AddProductToOrder(int id, int productId)
    {
        var orderToUpdate = _orderRepository.GetById(id);

        if (orderToUpdate is null)
        {
            throw new Exception($"Заказ с идентификатором {id} не найден");
        }

        var productToAdd = _productRepository.GetById(productId);

        if (productToAdd is null)
        {
            throw new Exception($"Товар с идентификатором {id} не найден");
        }

        _orderRepository.AddProductToOrder(orderToUpdate, productToAdd);
        _unitOfWork.Commit();
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

    public void RemoveProductFromOrder(int id, int productId)
    {
        var orderToUpdate = _orderRepository.GetById(id);

        if (orderToUpdate is null)
        {
            throw new Exception($"Заказ с идентификатором {id} не найден");
        }

        var productToRemove = _productRepository.GetById(productId);

        if (productToRemove is null)
        {
            throw new Exception($"Товар с идентификатором {id} не найден");
        }

        _orderRepository.RemoveProductFromOrder(orderToUpdate, productToRemove);
        _unitOfWork.Commit();
    }
}
