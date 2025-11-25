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

    [HttpGet("Get")]
    public async Task<ActionResult<IEnumerable<PaymentOrderDto>>> Get()
    {
        var paymentOrders = await _orderService.GetAsync();

        return Ok(paymentOrders);
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<PaymentOrderDto>> Get(int id)
    {
        var paymentOrder = await _orderService.GetAsync(id);

        return Ok(paymentOrder);
    }

    [HttpPut("Cancel/{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _orderService.CancelAsync(id);

        return NoContent();
    }

    [HttpPut("Pay/{id}")]
    public async Task<IActionResult> Pay(int id)
    {
        await _orderService.PayAsync(id);

        return NoContent();
    }
}