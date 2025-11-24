using ProductsOrders.Domain.Providers;
using ProductsOrders.Infrastructure.Payments;
using System.Reflection;

namespace ProductsOrders.UnitTests.Infrastructure.Factories;

public class ProviderFactoryTests(ProviderFactoryFixture fixture) : IClassFixture<ProviderFactoryFixture>
{
    private readonly ProviderFactoryFixture _fixture = fixture;

    [Theory]
    [MemberData(nameof(SupportedProviders))]
    public void GetProvider_ShouldReturnCorrectProvider_WhenProvidingValidProviderName(Provider providerParameter, Type expectedType)
    {
        var provider = _fixture.ServiceUnderTest.GetProvider(providerParameter);
        Assert.IsType(expectedType, provider);
    }

    [Fact]
    public void GetProvider_ShouldThrow_WhenProvidingNotSupportedProvider()
    {
        var unsupportedProvider = (Provider)Activator.CreateInstance(
            typeof(Provider),
            BindingFlags.NonPublic | BindingFlags.Instance,
            binder: null,
            args: new object[] { "NotSupportedProvider" },
            culture: null
        );

        Assert.Throws<ArgumentException>(() => _fixture.ServiceUnderTest.GetProvider(unsupportedProvider!));
    }

    public static IEnumerable<object[]> SupportedProviders =
    [
        [Provider.PagoFacil, typeof(PagoFacilExternalPaymentClient)],
        [Provider.CazaPagos, typeof(CazaPagoExternalPaymentClient)]
    ];
}