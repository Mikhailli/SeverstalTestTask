using System.Buffers.Text;

namespace WpfOrderManagementSystem.Services.Implementations;

internal class ServiceForOrderManagementApi
{
    public static string AddOrder(string baseUrl) => $"{baseUrl}/api/order";

    public static string DeleteOrder(string baseUrl, int id) => $"{baseUrl}/api/order/{id}";

    public static string ChangeStatus(string baseUrl, int id, string status) => $"{baseUrl}/api/order/{id}/{status}";

    public static string ChangeOrderItems(string baseUrl, int id) => $"{baseUrl}/api/order/{id}";
}
