namespace WpfOrderManagementSystem.Models;

internal class OrderItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}
