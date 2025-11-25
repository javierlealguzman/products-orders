using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Application.DTOs;
using ProductsOrders.Application.Services;
using ProductsOrders.Domain.DTOs;
using ProductsOrders.Domain.Entities;
using ProductsOrders.Domain.Factories;
using ProductsOrders.Domain.Providers;
using ProductsOrders.Domain.Strategies;

namespace ProductsOrders.Infrastructure.Services;

public class PaymentOrderService(
    IProviderSelector providerSelector, 
    IProviderFactory providerFactory, 
    IPaymentOrderRepository paymentOrderRepository
    ) : IPaymentOrderService
{
    private readonly IProviderSelector _providerSelector = providerSelector;
    private readonly IProviderFactory _providerFactory = providerFactory;
    private readonly IPaymentOrderRepository _paymentOrderRepository = paymentOrderRepository;

    public async Task<bool> CancelAsync(int id)
    {
        var paymentOrder = await _paymentOrderRepository.GetAsync(id);

        var provider = Provider.FromString(paymentOrder.Provider);

        var httpProvider = _providerFactory.GetProvider(provider);

        await httpProvider.CancelOrderAsync(paymentOrder.OrderId);

        await _paymentOrderRepository.UpdateAsync(paymentOrder.Id, "Cancelled");

        return true;
    }

    public async Task<PaymentOrderDto> CreateAsync(PaymentOrderRequestDto paymentOrderRequestDto)
    {
        var totalAmount = paymentOrderRequestDto.Products.Sum(x => x.UnitPrice);

        var provider = _providerSelector.GetBestProvider(paymentOrderRequestDto.PaymentType, totalAmount);

        var httpProvider = _providerFactory.GetProvider(provider.Provider);

        var request = new OrderRequestDto
        {
            PaymentType = paymentOrderRequestDto.PaymentType,
            Products = paymentOrderRequestDto.Products.Select(x => new Domain.DTOs.ProductDto { 
                Name = x.Name,
                UnitPrice = x.UnitPrice
            })
        };

        var orderId = await httpProvider.ProcessPaymentAsync(request);

        var paymentOrder = new PaymentOrder
        {
            OrderId = orderId,
            PaymentMode = paymentOrderRequestDto.PaymentType.ToString(),
            Provider = provider.Provider.Name,
            PaymentDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Status = "Pending"
        };

        await _paymentOrderRepository.CreateAsync(paymentOrder);

        var paymentOrderDto = new PaymentOrderDto
        {
            Id = paymentOrder.Id,
            OrderId = paymentOrder.OrderId,
            PaymentMode = paymentOrder.PaymentMode,
            PaymentDate= paymentOrder.PaymentDate,
            TotalAmount = paymentOrder.TotalAmount,
            Provider = paymentOrder.Provider,
            Status = paymentOrder.Status
        };

        return paymentOrderDto;
    }

    public async Task<IEnumerable<PaymentOrderDto>> GetAsync()
    {
        var paymentOrders = await _paymentOrderRepository.GetAsync();

        var paymentOrdersDto = paymentOrders.Select(x => new PaymentOrderDto 
        {
            Id = x.Id,
            OrderId = x.OrderId,
            PaymentMode = x.PaymentMode,
            PaymentDate = x.PaymentDate,
            TotalAmount = x.TotalAmount,
            Provider = x.Provider,
            Status = x.Status
        });

        return paymentOrdersDto;
    }

    public async Task<PaymentOrderDto> GetAsync(int id)
    {
        var paymentOrder = await _paymentOrderRepository.GetAsync(id);

        var paymentOrderDto = new PaymentOrderDto
        {
            Id = paymentOrder.Id,
            OrderId = paymentOrder.OrderId,
            PaymentMode = paymentOrder.PaymentMode,
            Provider = paymentOrder.Provider,
            PaymentDate = paymentOrder.PaymentDate,
            TotalAmount = paymentOrder.TotalAmount,
            Status = paymentOrder.Status
        };

        return paymentOrderDto;
    }

    public async Task<bool> PayAsync(int id)
    {
        var paymentOrder = await _paymentOrderRepository.GetAsync(id);

        var provider = Provider.FromString(paymentOrder.Provider);

        var httpProvider = _providerFactory.GetProvider(provider);

        await httpProvider.PayOrderAsync(paymentOrder.OrderId);

        await _paymentOrderRepository.UpdateAsync(paymentOrder.Id, "Completed");

        return true;
    }
}