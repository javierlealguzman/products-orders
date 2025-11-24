using Moq;
using ProductsOrders.Domain.Strategies;
using ProductsOrders.Infrastructure.Providers;

namespace ProductsOrders.UnitTests.Infrastructure.Strategies;

public class ProviderSelectorTests()
{
    [Fact]
    public void GetBestProvider_ShouldReturnProvider_WhenSupportingPaymentType()
    {
        var firstStrategy = new Mock<IProviderStrategy>();
        firstStrategy.Setup(x => x.HasPaymentSupport(Domain.Enums.PaymentType.Cash)).Returns(true);

        var secondStrategy = new Mock<IProviderStrategy>();
        secondStrategy.Setup(x => x.HasPaymentSupport(Domain.Enums.PaymentType.Cash)).Returns(false);

        var serviceUnderTest = new ProviderSelector([firstStrategy.Object, secondStrategy.Object]);

        var result = serviceUnderTest.GetBestProvider(Domain.Enums.PaymentType.Cash, 100m);

        Assert.NotNull(result);
        Assert.Equal(firstStrategy.Object, result);
    }

    [Fact]
    public void GetBestProvider_ShouldReturnBestProvider_WhenHasLowestCommission()
    {
        var firstStrategy = new Mock<IProviderStrategy>();
        firstStrategy.Setup(x => x.HasPaymentSupport(Domain.Enums.PaymentType.CreditCard)).Returns(true);
        firstStrategy.Setup(x => x.CalculateCommission(Domain.Enums.PaymentType.CreditCard, 1000m)).Returns(1000);

        var secondStrategy = new Mock<IProviderStrategy>();
        secondStrategy.Setup(x => x.HasPaymentSupport(Domain.Enums.PaymentType.CreditCard)).Returns(true);
        secondStrategy.Setup(x => x.CalculateCommission(Domain.Enums.PaymentType.CreditCard, 1000m)).Returns(10m);

        var serviceUnderTest = new ProviderSelector([firstStrategy.Object, secondStrategy.Object]);

        var result = serviceUnderTest.GetBestProvider(Domain.Enums.PaymentType.CreditCard, 1000m);
    }
}