namespace WpfOrderManagementSystem.Models;

internal class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime StorageUntil { get; set; }
    public string Status { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<OrderItem> Items { get; set; } = null!;
}
