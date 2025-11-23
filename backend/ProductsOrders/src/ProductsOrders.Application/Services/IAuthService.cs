namespace ProductsOrders.Application.Services;

public interface IAuthService
{
    Task<string> LoginAsync(string username, string password);
}