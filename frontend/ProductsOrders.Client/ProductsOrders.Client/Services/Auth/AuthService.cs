using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProductsOrders.Client.Models.Auth;
using System.Net.Http.Json;

namespace ProductsOrders.Client.Services.Auth;

public class AuthService(
    HttpClient http, 
    ILocalStorageService localStorage, 
    AuthenticationStateProvider authProvider)
{
    private readonly HttpClient _http = http;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly CustomAuthStateProvider _authProvider = (CustomAuthStateProvider) authProvider;

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new { username, password });

        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result == null) return false;

        await _localStorage.SetItemAsync("authToken", result.Token);

        _authProvider.NotifyAuthStateChanged();

        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _authProvider.NotifyAuthStateChanged();
    }
}