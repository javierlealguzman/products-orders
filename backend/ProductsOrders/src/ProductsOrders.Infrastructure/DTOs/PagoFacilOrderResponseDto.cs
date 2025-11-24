using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class PagoFacilOrderResponseDto
{
    [JsonProperty("orderId")]
    public Guid OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;

    [JsonProperty("fees")]
    public IEnumerable<PagoFacilFeeDto> Fees { get; set; } = [];

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; } = [];

    [JsonProperty("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;
}
