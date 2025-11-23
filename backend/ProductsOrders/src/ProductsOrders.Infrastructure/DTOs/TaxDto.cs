using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class TaxDto
{
    [JsonProperty("tax")]
    public string Tax { get; set; } = string.Empty;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
