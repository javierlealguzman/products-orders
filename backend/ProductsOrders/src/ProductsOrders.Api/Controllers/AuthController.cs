using Microsoft.AspNetCore.Mvc;
using ProductsOrders.Api.DTOs;
using ProductsOrders.Application.Services;

namespace ProductsOrders.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request.Username, request.Password);

        return Ok(new { token });
    }
}