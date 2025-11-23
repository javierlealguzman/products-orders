using ProductsOrders.Domain.Enums;
using ProductsOrders.Domain.Providers;
using ProductsOrders.Domain.Strategies;

namespace ProductsOrders.Infrastructure.Providers.Strategies;

public class CazaPagosStrategy : IProviderStrategy
{
    public string Name => Provider.CazaPagos.Name;

    public bool HasPaymentSupport(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => false,
            PaymentType.CreditCard => true,
            PaymentType.Transfer => true,
            _ => false
        };
    }

    public decimal CalculateCommission(PaymentType paymentType, decimal totalAmount)
    {
        return paymentType switch
        {
            PaymentType.CreditCard => CalculateCreditCardCommission(totalAmount),
            PaymentType.Transfer => CalculateTransferCommission(totalAmount),
            _ => decimal.MaxValue
        };
    }

    private static decimal CalculateCreditCardCommission(decimal totalAmount)
    {
        if (totalAmount <= 1500) return totalAmount * 0.02m;
        if (totalAmount <= 5000) return totalAmount * 0.015m;
        return totalAmount * 0.005m;
    }

    private static decimal CalculateTransferCommission(decimal totalAmount)
    {
        if (totalAmount <= 500) return 5m;
        if (totalAmount <= 1000) return totalAmount * 0.025m;
        return totalAmount * 0.02m;
    }
}