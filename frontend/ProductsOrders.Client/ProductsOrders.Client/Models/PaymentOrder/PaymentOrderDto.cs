namespace ProductsOrders.Client.Models.PaymentOrder;

public class PaymentOrderDto
{
    public int Id { get; set; }
    public string OrderId { get; set; } = string.Empty;
    public string PaymentMode { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}