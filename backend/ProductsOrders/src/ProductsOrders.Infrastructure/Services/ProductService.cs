using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Application.DTOs;
using ProductsOrders.Application.Services;

namespace ProductsOrders.Infrastructure.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<ProductDetailDto>> GetAsync()
    {
        var products = await _productRepository.GetAsync();

        if (products == null) return [];

        var productsDetailDto = products.Select(x => new ProductDetailDto 
        { 
            Id = x.Id,
            Name = x.Name,
            Details = x.Details,
            IsAvailable = x.IsAvailable,
            UnitPrice = x.UnitPrice
        });

        return productsDetailDto;
    }
}