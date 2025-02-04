using Common.DataAccess.Interfaces;
using Common.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.Implementations;

internal class EFOrderRepository(OrderDbContext context) : EFGenericRepository<Order>(context), IOrderRepository
{
    public Order CreateAndAdd(DateTime orderDate, DateTime storageUntil, string status, string? customerName, string? phoneNumber)
    {
        var order = new Order
        {
            OrderDate = orderDate,
            StorageUntil = storageUntil,
            Status = status,
            CustomerName = customerName,
            PhoneNumber = phoneNumber
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

    public override IEnumerable<Order> GetAll()
    {
        // По умолчанию не подгружает навигационные свойства, поэтому добавляем их
        return Get(includes:
        [
            source => source
            .Include(order => order.Items)
            .ThenInclude(item => item.Product)
        ]).AsEnumerable();
    }

    public override async Task<Order?> GetByIdAsync(object? id)
    {
        // По умолчанию не подгружает навигационные свойства, поэтому добавляем их
        return await _dbSet.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == (int)id!);
    }
}
