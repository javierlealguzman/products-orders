using Microsoft.EntityFrameworkCore;
using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Infrastructure.Persistence;

public class ProductsOrdersContext : DbContext
{
    public ProductsOrdersContext(DbContextOptions<ProductsOrdersContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PaymentOrder> PaymentOrders { get; set; }
}
