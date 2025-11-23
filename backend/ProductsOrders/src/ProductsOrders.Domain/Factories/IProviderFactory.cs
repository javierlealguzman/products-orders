using ProductsOrders.Domain.Payments;
using ProductsOrders.Domain.Providers;

namespace ProductsOrders.Domain.Factories;

public interface IProviderFactory
{
    IExternalPaymentClient GetProvider(Provider provider);
}