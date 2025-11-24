using ProductsOrders.Application.DTOs;

namespace ProductsOrders.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDetailDto>> GetAsync();
}