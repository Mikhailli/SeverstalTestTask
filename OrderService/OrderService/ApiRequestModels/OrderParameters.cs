namespace OrderService.ApiRequestModels;

public class OrderParameters
{
    public DateTime OrderDate { get; set; }
    public DateTime StorageUntil { get; set; }
    public string Status { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string? PhoneNumber { get; set; }
}
