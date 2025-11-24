using ProductsOrders.Domain.Enums;
using ProductsOrders.Infrastructure.Providers.Strategies;

namespace ProductsOrders.UnitTests.Infrastructure.Strategies;

public class PagoFacilStrategyTests
{
    private readonly PagoFacilStrategy _strategy = new();

    [Theory]
    [InlineData(PaymentType.Cash, true)]
    [InlineData(PaymentType.CreditCard, true)]
    [InlineData(PaymentType.Transfer, false)]
    public void HasPaymentSupport_ShouldReturnExpectedResult(PaymentType paymentType, bool expectedResult)
    {
        var result = _strategy.HasPaymentSupport(paymentType);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(PaymentType.Cash, 100, 15)]
    [InlineData(PaymentType.Cash, 12000, 15)]
    [InlineData(PaymentType.Cash, 50000, 15)]
    [InlineData(PaymentType.CreditCard, 100, 1)]
    [InlineData(PaymentType.CreditCard, 2000, 20)]
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