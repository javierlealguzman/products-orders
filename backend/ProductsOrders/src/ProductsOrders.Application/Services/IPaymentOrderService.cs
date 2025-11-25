using ProductsOrders.Application.DTOs;

namespace ProductsOrders.Application.Services;

public interface IPaymentOrderService
{
    Task<PaymentOrderDto> CreateAsync(PaymentOrderRequestDto paymentOrder);
    Task<IEnumerable<PaymentOrderDto>> GetAsync();
    Task<PaymentOrderDto> GetAsync(int id);
    Task<bool> CancelAsync(int id);
    Task<bool> PayAsync(int id);
}