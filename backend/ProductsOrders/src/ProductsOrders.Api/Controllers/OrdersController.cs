using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsOrders.Api.DTOs;
using ProductsOrders.Application.DTOs;
using ProductsOrders.Application.Services;

namespace ProductsOrders.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController(IPaymentOrderService orderService) : ControllerBase
{
    private readonly IPaymentOrderService _orderService = orderService;

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest orderRequest)
    {
        var paymentOrder = new PaymentOrderRequestDto {
            PaymentType = orderRequest.PaymentType,
            Products = orderRequest.Products.Select(x => new Application.DTOs.ProductDto { 
                Name = x.Name,
                UnitPrice = x.UnitPrice,
            })
        };

        var paymentOrderDto = await _orderService.CreateAsync(paymentOrder);

        return StatusCode(StatusCodes.Status201Created, paymentOrderDto);
    }
}