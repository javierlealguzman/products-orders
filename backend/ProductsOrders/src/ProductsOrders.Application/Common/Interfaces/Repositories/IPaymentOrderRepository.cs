using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Application.Common.Interfaces.Repositories;

public interface IPaymentOrderRepository
{
    Task<PaymentOrder> CreateAsync(PaymentOrder paymentOrder);
    Task<IEnumerable<PaymentOrder>> GetAsync();
    Task<PaymentOrder> GetAsync(int id);
    Task<PaymentOrder> UpdateAsync(int id, string status);
}