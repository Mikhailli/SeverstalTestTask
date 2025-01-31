using Common.DataAccess.Implementations;

namespace Common.DataAccess.Models;

public class Order : Entity
{
    public DateTime OrderDate { get; set; }
    public DateTime StorageUntil { get; set; }
    public string Status { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<OrderItem> Items { get; set; } = null!;
}
