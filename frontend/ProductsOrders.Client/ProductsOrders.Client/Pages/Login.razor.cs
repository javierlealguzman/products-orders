using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductsOrders.Client.Models.Auth;
using ProductsOrders.Client.Services.Auth;

namespace ProductsOrders.Client.Pages;

public partial class Login : ComponentBase
{
    private string? errorMessage;

    [Inject] public AuthService AuthService { get; set; } = null!;

    [Inject] public NavigationManager Navigation { get; set; } = null!;

    public MudForm? Form;

    private LoginRequest LoginRequest = new();

    private async Task LoginAction()
    {
        await Form.Validate();

        if (!Form.IsValid) return;

        errorMessage = null;

        var success = await AuthService.LoginAsync(LoginRequest.Username, LoginRequest.Password);

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
