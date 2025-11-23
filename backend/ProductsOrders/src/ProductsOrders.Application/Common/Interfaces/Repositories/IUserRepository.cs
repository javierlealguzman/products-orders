using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}