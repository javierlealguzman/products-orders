using Microsoft.EntityFrameworkCore;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Domain.Entities;

namespace ProductsOrders.Infrastructure.Persistence.Repositories;

public class PaymentOrderRepository(ProductsOrdersContext context) : IPaymentOrderRepository
{
    private readonly ProductsOrdersContext _context = context;

    public async Task<PaymentOrder> CreateAsync(PaymentOrder paymentOrder)
    {
        await _context.AddAsync(paymentOrder);
        await _context.SaveChangesAsync();
        return paymentOrder;
    }

    public async Task<IEnumerable<PaymentOrder>> GetAsync()
    {
        return await _context.PaymentOrders.AsNoTracking().ToListAsync();
    }
}