using Common.DataAccess.Models;

namespace Common.DataAccess.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    internal Order? CreateAndAdd(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber, ICollection<OrderItem> items);

    internal void Update(Order order, DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber, ICollection<OrderItem> items);

    internal void AddItemToOrder(Order order, Product product);

    internal void RemoveItemFromOrder(Order order, Product product);
}
