using Newtonsoft.Json;

namespace ProductsOrders.Infrastructure.DTOs;

public class OrderRequestDto
{
    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;

    [JsonProperty("products")]
    public IEnumerable<ProductDto> Products { get; set; } = [];
}