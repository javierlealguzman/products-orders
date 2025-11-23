using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class ProductDto
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("unitPrice")]
    public decimal UnitPrice { get; set; }
}
