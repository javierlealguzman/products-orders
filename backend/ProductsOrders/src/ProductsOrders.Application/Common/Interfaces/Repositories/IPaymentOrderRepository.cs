using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Application.Common.Interfaces.Repositories;

public interface IPaymentOrderRepository
{
    Task<PaymentOrder> CreateAsync(PaymentOrder paymentOrder);
}