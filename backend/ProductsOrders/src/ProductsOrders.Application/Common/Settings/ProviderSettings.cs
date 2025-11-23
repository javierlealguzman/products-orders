namespace ProductsOrders.Application.Common.Settings;

public class ProviderSettings
{
    public string Url { get; set; } = string.Empty;

    public Dictionary<string, string> Headers { get; set; } = [];
}