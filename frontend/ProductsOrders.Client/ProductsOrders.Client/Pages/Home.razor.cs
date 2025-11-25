using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ProductsOrders.Client.Pages;

public partial class Home : ComponentBase
{
    private string Username = string.Empty;
    private string Role = string.Empty;

    [Inject] public AuthenticationStateProvider AuthProvider { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var auth = await AuthProvider.GetAuthenticationStateAsync();
        Username = auth.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value ?? "Not found";
        Role = auth.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value ?? "No Role";
    }
}