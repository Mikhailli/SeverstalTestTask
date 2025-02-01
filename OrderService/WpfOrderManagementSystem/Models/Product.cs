namespace WpfOrderManagementSystem.Models;

internal class Product
{
    public int Id { get; set; }
    public string Article { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
