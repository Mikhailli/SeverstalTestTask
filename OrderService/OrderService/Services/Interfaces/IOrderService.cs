using Common.DataAccess.Models;
using OrderService.ApiRequestModels;

namespace OrderService.Services.Interfaces;

public interface IOrderService
{
    Order AddOrder(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber);

    void DeleteOrder(int id);

    void ChangeStatus(int id, string status);

    void ChangeOrderItems(int id, ICollection<OrderItemParameters> orderItemParameters);
}