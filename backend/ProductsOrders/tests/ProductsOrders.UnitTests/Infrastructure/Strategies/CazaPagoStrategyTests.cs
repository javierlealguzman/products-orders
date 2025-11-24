using ProductsOrders.Domain.Enums;
using ProductsOrders.Infrastructure.Providers.Strategies;

namespace ProductsOrders.UnitTests.Infrastructure.Strategies;

public class CazaPagoStrategyTests
{
    private readonly CazaPagosStrategy _strategy = new();

    [Theory]
    [InlineData(PaymentType.Cash, false)]
    [InlineData(PaymentType.CreditCard, true)]
    [InlineData(PaymentType.Transfer, true)]
    public void HasPaymentSupport_ShouldReturnExpectedResult(PaymentType paymentType, bool expectedResult)
    {
        var result = _strategy.HasPaymentSupport(paymentType);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(PaymentType.CreditCard, 100, 2)]
    [InlineData(PaymentType.CreditCard, 1500, 30)]
    [InlineData(PaymentType.CreditCard, 2000, 30)]
    [InlineData(PaymentType.CreditCard, 5000, 75)]
    [InlineData(PaymentType.CreditCard, 6000, 30)]
    [InlineData(PaymentType.Transfer, 100, 5)]
    [InlineData(PaymentType.Transfer, 500, 5)]
    [InlineData(PaymentType.Transfer, 600, 15)]
    [InlineData(PaymentType.Transfer, 1000, 25)]
    [InlineData(PaymentType.Transfer, 1500, 30)]
    public void CalculateCommission_ShouldReturnExpectedCommission(PaymentType paymentType, decimal totalAmount, decimal expectedCommission)
    {
        var result = _strategy.CalculateCommission(paymentType, totalAmount);

        Assert.Equal(expectedCommission, result);
    }

    [Fact]
    public void CalculateCommission_ShouldReturnMaxValue_WhenProvidingInvalidPaymentType()
    {
        var result = _strategy.CalculateCommission((PaymentType)10, 2000);

        Assert.Equal(decimal.MaxValue, result);
    }
}
