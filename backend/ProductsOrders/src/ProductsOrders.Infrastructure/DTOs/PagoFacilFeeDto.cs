using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class PagoFacilFeeDto
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}