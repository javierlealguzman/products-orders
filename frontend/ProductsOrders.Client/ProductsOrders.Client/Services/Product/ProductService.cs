using Blazored.LocalStorage;
using ProductsOrders.Client.Models.Product;
using System.Net.Http.Json;

namespace ProductsOrders.Client.Services.Product;

public class ProductService(HttpClient http, ILocalStorageService localStorage)
{
    private readonly HttpClient _http = http;
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        _http.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var products = await _http.GetFromJsonAsync<List<ProductDto>>("api/Products");

        if (products == null) return [];

        return products;
    }
}