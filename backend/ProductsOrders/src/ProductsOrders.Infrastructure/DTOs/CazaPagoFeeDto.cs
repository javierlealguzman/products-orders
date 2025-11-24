using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class CazaPagoFeeDto
{
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}