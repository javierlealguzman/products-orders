using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class CazaPagoOrderResponseDto
{
    [JsonProperty("orderId")]
    public string OrderId { get; set; } = string.Empty;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;

    [JsonProperty("fees")]
    public IEnumerable<CazaPagoFeeDto> Fees { get; set; } = [];

    [JsonProperty("taxes")]
    public IEnumerable<TaxDto> Taxes { get; set; } = [];

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; } = [];

    [JsonProperty("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;
}