using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

public class OrderItem : Entity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int Quantity { get; set; }
}
