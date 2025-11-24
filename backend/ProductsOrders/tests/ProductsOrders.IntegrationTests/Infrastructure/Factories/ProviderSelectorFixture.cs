using ProductsOrders.Infrastructure.Providers;
using ProductsOrders.Infrastructure.Providers.Strategies;

namespace ProductsOrders.IntegrationTests.Infrastructure.Factories;

public class ProviderSelectorFixture
{
    public ProviderSelector ServiceUnderTest { get; }
    public ProviderSelectorFixture()
    {
        var pagoFacilStrategy = new PagoFacilStrategy();
        var cazapagoStrategy = new CazaPagosStrategy();

        ServiceUnderTest = new ProviderSelector([
            pagoFacilStrategy, cazapagoStrategy
        ]);   
    }
}