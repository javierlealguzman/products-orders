namespace ProductsOrders.Domain.Entities;

public class PaymentOrder
{
    public int Id { get; set; }
    public string PaymentMode { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}