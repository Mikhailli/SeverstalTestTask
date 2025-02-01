using System.Net.Http;
using Newtonsoft.Json;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.Settings;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.Services.Implementations;

internal class OrderServiceForInformation(HttpClient httpClient, ServiceForInformationApiSettings apiSettings) : IOrderServiceForInformation
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseUrl = apiSettings.BaseUrl;

    public async Task<Order[]> GetAllAsync()
    {
        var requestUrl = ServiceForInformationApi.Orders.GetAll(_baseUrl);

        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<Order[]>(jsonString);
            return orders!;
        }

        var responseString = await response.Content.ReadAsStringAsync();
        throw new Exception("Ошибка получения списка заказов");
    }

    public async Task<Order> GetAsync(int id)
    {
        var requestUrl = ServiceForInformationApi.Orders.Get(_baseUrl, id);

        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(jsonString);
            return order!;
        }

        var responseString = response.Content.ReadAsStringAsync().Result;
        throw new Exception($"Ошибка получения заказа с id {id}");
    }
}
