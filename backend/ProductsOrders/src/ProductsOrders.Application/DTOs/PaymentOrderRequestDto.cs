using ProductsOrders.Domain.Enums;

namespace ProductsOrders.Application.DTOs
{
    public class PaymentOrderRequestDto
    {
        public PaymentType PaymentType { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } = [];
    }
}