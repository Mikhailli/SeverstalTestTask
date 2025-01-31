using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;

namespace Common.DataAccess.Implementations;

internal class EFOrderRepository : EFGenericRepository<Order>, IOrderRepository
{
    public EFOrderRepository(OrderDbContext context) : base(context)
    {
    }

    public Order? CreateAndAdd(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? PhoneNumber, ICollection<OrderItem> items)
    {
        var order = new Order
        {
            OrderDate = orderDate,
            StorageUntil = storageUntil,
            Status = status,
            CustomerName = customerName,
            PhoneNumber = PhoneNumber,
            Items = items
        };

        Add(order);

        return order;
    }

    public void Update(Order order, DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? phoneNumber, ICollection<OrderItem> items)
    {
        order.OrderDate = orderDate;
        order.StorageUntil = storageUntil;
        order.Status = status;
        order.CustomerName = customerName;
        order.PhoneNumber = phoneNumber;
        order.Items = items;

        Update(order);
    }

    public void AddItemToOrder(Order order, Product product)
    {
        var item = order.Items.FirstOrDefault(item => item.ProductId == product.Id);
        if (item is null)
        {
            order.Items.Add(new OrderItem() { Product = product, Order = order, Quantity = 1 });
        }
        else
        {
            item.Quantity += 1;
        }

        Update(order);
    }


    public void RemoveItemFromOrder(Order order, Product product)
    {
        var item = order.Items.FirstOrDefault(item => item.ProductId == product.Id);
        if (item is null)
        {
            return;
        }
        else if (item.Quantity == 1)
        {
            order.Items.Remove(item);
        }
        else
        {
            item.Quantity -= 1;
        }

        Update(order);
    }
}
