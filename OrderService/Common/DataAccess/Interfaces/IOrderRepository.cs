using Common.DataAccess.Models;

namespace Common.DataAccess.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    public Order CreateAndAdd(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber);

    public void Update(Order order, DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber, ICollection<OrderItem> items);

    public void AddProductToOrder(Order order, Product product);

    public void RemoveProductFromOrder(Order order, Product product);
}
