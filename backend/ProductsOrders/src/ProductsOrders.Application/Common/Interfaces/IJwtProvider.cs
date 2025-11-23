namespace ProductsOrders.Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(int userId, string username, string role);
}
