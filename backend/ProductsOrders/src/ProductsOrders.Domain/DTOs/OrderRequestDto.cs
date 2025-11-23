using ProductsOrders.Domain.Enums;

namespace ProductsOrders.Domain.DTOs;

public class OrderRequestDto
{
    public PaymentType PaymentType { get; set; }
    public IEnumerable<ProductDto> Products { get; set; } = [];
}