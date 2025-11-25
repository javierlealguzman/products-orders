namespace ProductsOrders.Client.Models.PaymentOrder;

public class ProductRequestDto
{
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}