using Microsoft.EntityFrameworkCore;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Infrastructure.Persistence.Repositories;

public class UserRepository(ProductsOrdersContext context) : IUserRepository
{
    private readonly ProductsOrdersContext _context = context;

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
    }
}