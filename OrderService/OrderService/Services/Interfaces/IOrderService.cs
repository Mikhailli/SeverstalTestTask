using Common.DataAccess.Models;

namespace ProductService.Services.Interfaces;

public interface IOrderService
{
    Order AddOrder(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber);

    void DeleteOrder(int id);

    void ChangeStatus(int id, string status);

    void AddProductToOrder(int id, int producrId);

    void RemoveProductFromOrder(int id, int productId);
}