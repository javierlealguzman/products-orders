using ProductsOrders.Domain.Factories;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Domain.Providers;
using ProductsOrders.Infrastructure.Payments;

namespace ProductsOrders.Infrastructure.Factories;

public class ProviderFactory(IEnumerable<IExternalPaymentClient> providers) : IProviderFactory
{
    private readonly IEnumerable<IExternalPaymentClient> _providers = providers;

    public IExternalPaymentClient GetProvider(Provider provider)
    {
        return provider.Name switch
        {
            "PagoFacil" => _providers.OfType<PagoFacilExternalPaymentClient>().First(),
            "CazaPago" => _providers.OfType<CazaPagoExternalPaymentClient>().First(),
            _ => throw new ArgumentException("Provider not supported")
        };
    }
}