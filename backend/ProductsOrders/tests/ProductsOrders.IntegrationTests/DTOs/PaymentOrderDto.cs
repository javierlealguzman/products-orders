using System.Text.Json.Serialization;

namespace ProductsOrders.IntegrationTests.DTOs;

public class PaymentOrderDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;

    [JsonPropertyName("paymentMode")]
    public string PaymentMode { get; set; } = string.Empty;

    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;

    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("paymentDate")]
    public DateTime PaymentDate { get; set; }
}