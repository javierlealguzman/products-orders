using ProductsOrders.Domain.DTOs;

namespace ProductsOrders.Domain.Payments;

public interface IExternalPaymentClient
{
    Task<string> ProcessPaymentAsync(OrderRequestDto orderRequest);
    Task<IEnumerable<OrderDetailDto>> GetOrdersAsync();
    Task<OrderDetailDto> GetOrderAsync(string id);
    Task<bool> CancelOrderAsync(string id);
    Task<bool> PayOrderAsync(string id);
}