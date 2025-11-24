using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAsync();
}