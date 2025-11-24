using ProductsOrders.Domain.Enums;
using ProductsOrders.Domain.Providers;

namespace ProductsOrders.Domain.Strategies;

public interface IProviderStrategy
{
    Provider Provider { get; }
    bool HasPaymentSupport(PaymentType paymentType);
    decimal CalculateCommission(PaymentType paymentType, decimal totalAmount);
}