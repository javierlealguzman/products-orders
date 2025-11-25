using Microsoft.EntityFrameworkCore;
using ProductsOrders.Application.Common.Interfaces.Repositories;
using ProductsOrders.Domain.Entities;
using ProductsOrders.Domain.Exceptions;

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

    public async Task<PaymentOrder> GetAsync(int id)
    {
        var order = await _context.PaymentOrders.SingleOrDefaultAsync(x => x.Id == id);

        return order is null 
            ? throw new NotFoundException($"Order with id {id} not found") 
            : order;
    }

    public async Task<PaymentOrder> UpdateAsync(int id, string status)
    {
        var order = _context.PaymentOrders.SingleOrDefault(x => x.Id == id) ??
            throw new NotFoundException($"Order with id {id} not found");

        order.Status = status;

        _context.PaymentOrders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }
}