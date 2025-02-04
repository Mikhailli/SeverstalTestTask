using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.ApiRequestModels;
using WpfOrderManagementSystem.Models.Settings;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.Services.Implementations;

internal class ServiceForOrderManagement(HttpClient httpClient, ServiceForOrderManagementApiSettings apiSettings) : IServiceForOrderManagement
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseUrl = apiSettings.BaseUrl;

    public async Task<Order> AddOrderAsync(OrderParameters parameters)
    {
        var requestUrl = ServiceForOrderManagementApi.AddOrder(_baseUrl);

        var jsonString = JsonConvert.SerializeObject(parameters);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(requestUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonOrder = await response.Content.ReadAsStringAsync();
            var addedOrder = JsonConvert.DeserializeObject<Order>(jsonOrder);
            return addedOrder!;
        }

        var responseString = response.Content.ReadAsStringAsync().Result;
        throw new Exception("Ошибка при добавлении заказа");
    }

    public async Task ChangeStatusAsync(int id, string status)
    {
        var requestUrl = ServiceForOrderManagementApi.ChangeStatus(_baseUrl, id, status);

        var response = await _httpClient.PutAsync(requestUrl, null);

        if (response.IsSuccessStatusCode is false)
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            throw new Exception("Ошибка при изменении статуса");
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var requestUrl = ServiceForOrderManagementApi.DeleteOrder(_baseUrl, id);

        var response = await _httpClient.DeleteAsync(requestUrl);

        if (response.IsSuccessStatusCode is false)
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            throw new Exception("Ошибка при удалении заказа");
        }
    }

    public async Task ChangeOrderItemsAsync(int id, ICollection<OrderItemParameters> orderItems)
    {
        var requestUrl = ServiceForOrderManagementApi.ChangeOrderItems(_baseUrl, id);

        var jsonString = JsonConvert.SerializeObject(orderItems);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(requestUrl, content);

        if (response.IsSuccessStatusCode is false)
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            throw new Exception("Ошибка при изменении заказа");
        }
    }
}
