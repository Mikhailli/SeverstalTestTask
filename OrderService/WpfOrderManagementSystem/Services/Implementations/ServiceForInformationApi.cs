namespace WpfOrderManagementSystem.Services.Implementations;

internal static class ServiceForInformationApi
{
    public static class Orders
    {
        public static string Get(string baseUrl, int id) => $"{baseUrl}/api/order/{id}";

        public static string GetAll(string baseUrl) => $"{baseUrl}/api/order";
    }

    public static class Products
    {
        public static string Get(string baseUrl, int id) => $"{baseUrl}/api/product/{id}";

        public static string GetAll(string baseUrl) => $"{baseUrl}/api/product";
    }
}
