using Microsoft.EntityFrameworkCore;
using ProductsOrders.Application.Common.Interfaces;
using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Infrastructure.Persistence.Seed;

public class DbSeed(ProductsOrdersContext context, IPasswordHasher hasher)
{
    private readonly ProductsOrdersContext _context = context;
    private readonly IPasswordHasher _hasher = hasher;

    public async Task SeedAsync()
    {
        await SeedUsersAsync();
    }

    private async Task SeedUsersAsync()
    {
        if (await _context.Users.AnyAsync()) return;

        var admin = new User
        {
            Username = "admin@system.com",
            PasswordHash = _hasher.Hash("admin@123"),
            Role = "Admin"
        };

        _context.Users.Add(admin);

        await _context.SaveChangesAsync();
    }
}
