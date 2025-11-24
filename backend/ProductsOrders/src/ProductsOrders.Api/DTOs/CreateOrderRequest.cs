using ProductsOrders.Domain.Enums;

namespace ProductsOrders.Api.DTOs;

public class CreateOrderRequest
{
    public PaymentType PaymentType { get; set; }
    public IEnumerable<ProductDto> Products { get; set; } = [];
}