using Microsoft.Extensions.DependencyInjection;
using ProductsOrders.Api.DTOs;
using ProductsOrders.Application.Common.Interfaces;
using ProductsOrders.IntegrationTests.DTOs;
using System.Text;
using System.Text.Json;

namespace ProductsOrders.IntegrationTests.Api;

public class OrdersControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory = factory;

    [Fact]
    public async Task CreateOrder_ShouldReturnOrderId_With201StatusCode()
    {
        var client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();

        var jwtService = scope.ServiceProvider.GetRequiredService<IJwtProvider>();

        var jwt = jwtService.GenerateToken(1, "username", "role");

        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

        var body = new CreateOrderRequest
        {
            PaymentType = Domain.Enums.PaymentType.Cash,
            Products = [
                new ProductDto { Name = "Laptop", UnitPrice = 100m },
                new ProductDto { Name = "Desk", UnitPrice = 200m }
            ]
        };

        var jsonBody = JsonSerializer.Serialize(body);

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/Orders/Create", content);

        response.EnsureSuccessStatusCode();

        var stringResponse = await response.Content.ReadAsStringAsync();

        var responseObject = JsonSerializer.Deserialize<PaymentOrderDto>(stringResponse);

        var orderIds = new[] { _factory.CazaPagoOrderId, _factory.PagoFacilOrderId };
        
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(responseObject);
        Assert.NotEmpty(responseObject.OrderId);
        Assert.Contains(responseObject.OrderId, orderIds);
    }
}