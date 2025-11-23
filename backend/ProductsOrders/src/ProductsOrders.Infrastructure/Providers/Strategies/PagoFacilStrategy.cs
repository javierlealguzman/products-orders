using ProductsOrders.Domain.Enums;
using ProductsOrders.Domain.Strategies;

namespace ProductsOrders.Infrastructure.Providers.Strategies;

public class PagoFacilStrategy : IProviderStrategy
{
    public string Name => "PagoFacil";

    public bool HasPaymentSupport(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => true,
            PaymentType.CreditCard => true,
            PaymentType.Transfer => false,
            _ => false
        };
    }

    public decimal CalculateCommission(PaymentType paymentType, decimal totalAmount)
    {
        return paymentType switch
        {
            PaymentType.Cash => 15m,
            PaymentType.CreditCard => totalAmount * 0.01m,
            _ => decimal.MaxValue
        };
    }
}
