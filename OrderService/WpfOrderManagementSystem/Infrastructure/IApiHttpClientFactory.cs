using System.Net.Http;

namespace WpfOrderManagementSystem.Infrastructure;

internal interface IApiHttpClientFactory
{
    HttpClient GetUnauthorizedClient();
}
