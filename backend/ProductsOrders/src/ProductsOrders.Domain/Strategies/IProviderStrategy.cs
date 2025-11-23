using ProductsOrders.Domain.Enums;

namespace ProductsOrders.Domain.Strategies;

public interface IProviderStrategy
{
    string Name { get; }
    bool HasPaymentSupport(PaymentType paymentType);
    decimal CalculateCommission(PaymentType paymentType, decimal totalAmount);
}
