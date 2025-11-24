using ProductsOrders.Domain.Enums;
using ProductsOrders.Domain.Providers;

namespace ProductsOrders.IntegrationTests.Infrastructure.Factories;

public class ProviderSelectorTests(ProviderSelectorFixture fixture) : IClassFixture<ProviderSelectorFixture>
{
    private readonly ProviderSelectorFixture _fixture = fixture;

    [Theory]
    [MemberData(nameof(PaymentTypeOrders))]
    public void GetBestProvider_ShouldReturnProvider_WhenProvidingSupportedType(Provider expectedProvider, PaymentType paymentType, decimal totalAmount)
    {
        var result = _fixture.ServiceUnderTest.GetBestProvider(paymentType, totalAmount);

        Assert.Equal(expectedProvider, result.Provider);
    }

    [Theory]
    [MemberData(nameof(CommissionOrders))]
    public void GetBestProvider_ShouldReturnBestProviderBasedOnCommision(Provider expectedProvider, PaymentType paymentType, decimal totalAmount)
    {
        var result = _fixture.ServiceUnderTest.GetBestProvider(paymentType, totalAmount);

        Assert.Equal(expectedProvider, result.Provider);
    }

    public static IEnumerable<object[]> PaymentTypeOrders = [
        [Provider.PagoFacil, PaymentType.Cash, 5000],
        [Provider.CazaPagos, PaymentType.Transfer, 5000],
        [Provider.CazaPagos, PaymentType.CreditCard, 6000]
    ];

    public static IEnumerable<object[]> CommissionOrders = [
        [Provider.PagoFacil, PaymentType.CreditCard, 1000],
        [Provider.PagoFacil, PaymentType.CreditCard, 3000],
        [Provider.CazaPagos, PaymentType.CreditCard, 6000],
    ];
}