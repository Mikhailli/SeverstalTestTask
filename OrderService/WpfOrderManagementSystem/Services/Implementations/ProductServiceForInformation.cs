using System.Net.Http;
using Newtonsoft.Json;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.Settings;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.Services.Implementations;

internal class ProductServiceForInformation(HttpClient httpClient, ServiceForInformationApiSettings apiSettings) : IProductServiceForInformation
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseUrl = apiSettings.BaseUrl;

    public async Task<Product[]> GetAllAsync()
    {
        var requestUrl = ServiceForInformationApi.Products.GetAll(_baseUrl);

        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<Product[]>(jsonString);
            return products!;
        }

        var responseString = await response.Content.ReadAsStringAsync();
        throw new Exception("Ошибка получения списка товаров");
    }

    public async Task<Product> GetAsync(int id)
    {
        var requestUrl = ServiceForInformationApi.Products.Get(_baseUrl, id);

        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(jsonString);
            return product!;
        }

        var responseString = response.Content.ReadAsStringAsync().Result;
        throw new Exception($"Ошибка получения товара с id {id}");
    }
}
