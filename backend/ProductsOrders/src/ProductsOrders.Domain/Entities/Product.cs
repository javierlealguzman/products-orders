namespace ProductsOrders.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public decimal UnitPrice { get; set; }
}
