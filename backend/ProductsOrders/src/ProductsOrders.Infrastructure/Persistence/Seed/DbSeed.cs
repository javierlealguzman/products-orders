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
        await SeedProductsAsync();
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

    private async Task SeedProductsAsync()
    {
        if (await _context.Products.AnyAsync()) return;

        var products = new List<Product>
        {
            new() { Id = 1, Name = "Gaming Laptop", Details = "Laptop with RTX 4070, 16GB RAM", IsAvailable = true, UnitPrice = 1500m },
            new() { Id = 2, Name = "Wireless Mouse", Details = "Bluetooth mouse with adjustable DPI", IsAvailable = true, UnitPrice = 25m },
            new() { Id = 3, Name = "Mechanical Keyboard", Details = "Keyboard with tactile switches", IsAvailable = true, UnitPrice = 70m },
            new() { Id = 4, Name = "Sports T-Shirt", Details = "Lightweight t-shirt for sports", IsAvailable = true, UnitPrice = 20m },
            new() { Id = 5, Name = "Jeans Pants", Details = "Classic dark blue jeans", IsAvailable = false, UnitPrice = 40m },
            new() { Id = 6, Name = "Running Shoes", Details = "Comfortable sports shoes", IsAvailable = true, UnitPrice = 80m },
            new() { Id = 7, Name = "Bluetooth Headphones", Details = "Noise-cancelling headphones", IsAvailable = false, UnitPrice = 120m },
            new() { Id = 8, Name = "27-inch Monitor", Details = "IPS Full HD monitor 144Hz", IsAvailable = true, UnitPrice = 300m },
            new() { Id = 9, Name = "Espresso Coffee Machine", Details = "15-bar domestic coffee machine", IsAvailable = true, UnitPrice = 150m },
            new() { Id = 10, Name = "Office Chair", Details = "Ergonomic adjustable office chair", IsAvailable = true, UnitPrice = 90m },
            new() { Id = 11, Name = "Programming Book", Details = "Learn C# and .NET 8", IsAvailable = false, UnitPrice = 35m },
            new() { Id = 12, Name = "Premium Pen", Details = "Fine black ink pen", IsAvailable = true, UnitPrice = 10m }
        };

        await _context.Products.AddRangeAsync(products);

        await _context.SaveChangesAsync();
    }
}