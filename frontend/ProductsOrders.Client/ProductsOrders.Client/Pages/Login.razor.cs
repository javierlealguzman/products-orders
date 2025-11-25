using Microsoft.AspNetCore.Components;
using ProductsOrders.Client.Services.Auth;

namespace ProductsOrders.Client.Pages;

public partial class Login : ComponentBase
{
    public string Username = string.Empty;
    private string Password = string.Empty;
    private string? errorMessage;

    [Inject] public AuthService AuthService { get; set; } = null!;

    [Inject] public NavigationManager Navigation { get; set; } = null!;
    
    private async Task LoginAction()
    {
        errorMessage = null;

        var success = await AuthService.LoginAsync(Username, Password);

        if (success)
        {
            Navigation.NavigateTo("/");
        }
        else
        {
            errorMessage = "Invalid username or password";
        }
    }
}
