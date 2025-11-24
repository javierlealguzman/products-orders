using Microsoft.EntityFrameworkCore;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Infrastructure.Persistence.Repositories;

public class ProductRepository(ProductsOrdersContext context) : IProductRepository
{
    private readonly ProductsOrdersContext _context = context;

    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }
}