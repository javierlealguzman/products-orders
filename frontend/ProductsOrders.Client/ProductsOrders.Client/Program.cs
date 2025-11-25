using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductsOrders.Client;
using MudBlazor.Services;
using ProductsOrders.Client.Services.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProductsOrders.Client.Services.Product;
using MudBlazor;
using ProductsOrders.Client.Services.PaymentOrder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<JwtParserService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<PaymentOrderService>();

var apiBaseurl = builder.Configuration["ProductsBaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient
{ 
    BaseAddress = new Uri(apiBaseurl) 
});

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();