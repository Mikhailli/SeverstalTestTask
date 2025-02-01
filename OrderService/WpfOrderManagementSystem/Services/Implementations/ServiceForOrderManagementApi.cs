using System.Buffers.Text;

namespace WpfOrderManagementSystem.Services.Implementations;

internal class ServiceForOrderManagementApi
{
    public static string AddOrder(string baseUrl) => $"{baseUrl}/api/order";

    public static string DeleteOrder(string baseUrl, int id) => $"{baseUrl}/api/order/{id}";

    public static string ChangeStatus(string baseUrl, int id, string status) => $"{baseUrl}/api/order/{id}/{status}";

    public static string AddProductToOrder(string baseUrl, int id, int productId) => $"{baseUrl}/api/order/addProduct/{id}{productId}";

    public static string RemoveProductFromOrder(string baseUrl, int id, int productId) => $"{baseUrl}/api/order/removeProduct/{id}{productId}";
}
