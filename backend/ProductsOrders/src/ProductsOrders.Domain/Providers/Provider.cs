namespace ProductsOrders.Domain.Providers;

public sealed class Provider
{
    public string Name { get; } = string.Empty;

    private Provider(string name) => Name = name;

    public static readonly Provider PagoFacil = new("PagoFacil");

    public static readonly Provider CazaPagos = new("CazaPagos");

    public override string ToString() => Name;
}