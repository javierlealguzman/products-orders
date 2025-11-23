using ProductsOrders.Domain.Enums;
using ProductsOrders.Domain.Strategies;

namespace ProductsOrders.Infrastructure.Providers;

public class ProviderSelector(IEnumerable<IProviderStrategy> providers) : IProviderSelector
{
    private readonly IEnumerable<IProviderStrategy> _providers = providers;

    public IProviderStrategy GetBestProvider(PaymentType paymentType, decimal totalAmount)
    {
        return _providers
            .Where(x => x.HasPaymentSupport(paymentType))
            .OrderBy(x => x.CalculateCommission(paymentType, totalAmount))
            .First();
    }
}