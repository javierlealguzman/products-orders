using Newtonsoft.Json;
using ProductsOrders.Domain.DTOs;
using ProductsOrders.Domain.Exceptions;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Domain.Providers;
using ProductsOrders.Infrastructure.DTOs;
using System.Text;

namespace ProductsOrders.Infrastructure.Payments;

public class PagoFacilExternalPaymentClient(IHttpClientFactory httpFactory) : IExternalPaymentClient
{
    private readonly IHttpClientFactory _httpFactory = httpFactory;

    public async Task<bool> CancelOrderAsync(string id)
    {
        try
        {
            var client = _httpFactory.CreateClient(Provider.PagoFacil.Name);

            var response = await client.PutAsync($"/cancel?id={id}", null);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            throw new ExternalProviderException("Something happened while trying to cancel order.");
        }
    }

    public async Task<OrderDetailDto> GetOrderAsync(string id)
    {
        try
        {
            var client = _httpFactory.CreateClient(Provider.PagoFacil.Name);

            var response = await client.GetAsync($"/Order/{id}");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var order = JsonConvert.DeserializeObject<PagoFacilOrderResponseDto>(stringResponse)!;

            var orderDto = new OrderDetailDto
            {
                OrderId = order.OrderId.ToString(),
                Amount = order.Amount,
                Method = order.Method,
                Status = order.Status
            };

            return orderDto;
        }
        catch (HttpRequestException)
        {
            throw new ExternalProviderException("Something happened while trying to retrieve order.");
        }
        catch(JsonException)
        {
            throw new ExternalProviderException("Something happened while parsing response.");
        }
    }

    public async Task<IEnumerable<OrderDetailDto>> GetOrdersAsync()
    {
        try
        {
            var client = _httpFactory.CreateClient(Provider.PagoFacil.Name);

            var response = await client.GetAsync("/Order");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var orders = JsonConvert.DeserializeObject<IEnumerable<PagoFacilOrderResponseDto>>(stringResponse)!;

            var ordersDto = orders.Select(x => new OrderDetailDto
            {
                OrderId = x.OrderId.ToString(),
                Amount = x.Amount,
                Method = x.Method,
                Status = x.Status
            });

            return ordersDto;
        }
        catch (HttpRequestException)
        {
            throw new ExternalProviderException("Something happened while trying to retrieve orders.");
        }
        catch (JsonException)
        {
            throw new ExternalProviderException("Something happened while parsing response.");
        }
    }

    public async Task<bool> PayOrderAsync(string id)
    {
        try
        {
            var client = _httpFactory.CreateClient(Provider.PagoFacil.Name);

            var response = await client.PutAsync($"/pay?id={id}", null);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            throw new ExternalProviderException("Something happened while trying to pay order.");
        }
    }

    public virtual async Task<string> ProcessPaymentAsync(Domain.DTOs.OrderRequestDto orderRequest)
    {
        try
        {
            var client = _httpFactory.CreateClient(Provider.PagoFacil.Name);

            var orderRequestDto = new DTOs.OrderRequestDto
            {
                Method = orderRequest.PaymentType.ToString(),
                Products = orderRequest.Products.Select(x => new DTOs.ProductDto
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice
                })
            };

            var bodyContent = JsonConvert.SerializeObject(orderRequestDto);

            var stringContent = new StringContent(bodyContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Order", stringContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var order = JsonConvert.DeserializeObject<PagoFacilOrderResponseDto>(stringResponse);

            return order is null
                ? throw new ExternalProviderException("Unable to parse response from Pago Facil provider")
                : order.OrderId.ToString();
        }
        catch(HttpRequestException)
        {
            throw new ExternalProviderException("Something happened while trying to create order with pago facil provider");
        }
        catch(JsonException)
        {
            throw new ExternalProviderException("Something happened while parsing response");
        }
    }
}