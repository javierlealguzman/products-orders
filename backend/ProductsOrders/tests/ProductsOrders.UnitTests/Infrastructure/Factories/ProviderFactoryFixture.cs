using Moq;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Infrastructure.Factories;
using ProductsOrders.Infrastructure.Payments;

namespace ProductsOrders.UnitTests.Infrastructure.Factories;

public class ProviderFactoryFixture
{
    public ProviderFactory ServiceUnderTest { get; }
    public ProviderFactoryFixture()
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var pagoFacil = new PagoFacilExternalPaymentClient(httpClientFactoryMock.Object);
        var cazaPago = new CazaPagoExternalPaymentClient(httpClientFactoryMock.Object);

        ServiceUnderTest = new ProviderFactory([pagoFacil, cazaPago]);
    }
}