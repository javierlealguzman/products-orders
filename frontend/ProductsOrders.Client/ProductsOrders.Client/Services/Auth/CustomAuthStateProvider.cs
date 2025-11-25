using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ProductsOrders.Client.Services.Auth;

public class CustomAuthStateProvider(ILocalStorageService localStorage, JwtParserService jwtParserService) : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly JwtParserService _jwtParseService = jwtParserService;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = _jwtParseService.ParseClaims(token);

        var identity = new ClaimsIdentity(claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyAuthStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
