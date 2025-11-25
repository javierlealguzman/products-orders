using Blazored.LocalStorage;
using ProductsOrders.Client.Models.PaymentOrder;
using System.Net.Http.Json;

namespace ProductsOrders.Client.Services.PaymentOrder;

public class PaymentOrderService(HttpClient httpClient, ILocalStorageService localStorage)
{
    private readonly HttpClient _http = httpClient;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task<PaymentOrderDto> CreatePaymentOrderAsync(PaymentOrderRequest orderRequestDto)
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _http.PostAsJsonAsync("api/Orders/Create", orderRequestDto);

            var paymentOrderDto = await response.Content.ReadFromJsonAsync<PaymentOrderDto>();

            return paymentOrderDto!;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Unable to connect to api server", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }
}