using ProductsOrders.Client.Models.Enums;

namespace ProductsOrders.Client.Models.PaymentOrder;

public class PaymentOrderRequest
{
    public PaymentType PaymentType { get; set; }
    public List<ProductRequestDto> Products { get; set; } = [];
}