using Newtonsoft.Json;
using ProductsOrders.Domain.Exceptions;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Domain.Providers;
using ProductsOrders.Infrastructure.DTOs;
using System.Text;

namespace ProductsOrders.Infrastructure.Payments;

public class PagoFacilExternalPaymentClient(IHttpClientFactory httpFactory) : IExternalPaymentClient
{
    private readonly IHttpClientFactory _httpFactory = httpFactory;

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