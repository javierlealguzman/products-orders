using ProductsOrders.Domain.DTOs;

namespace ProductsOrders.Domain.Payments;

public interface IExternalPaymentClient
{
    Task<string> ProcessPaymentAsync(OrderRequestDto orderRequest);
}