using ProductsOrders.Domain.Enums;

namespace ProductsOrders.Domain.Strategies;

public interface IProviderSelector
{
    IProviderStrategy GetBestProvider(PaymentType paymentType, decimal totalAmount);
}