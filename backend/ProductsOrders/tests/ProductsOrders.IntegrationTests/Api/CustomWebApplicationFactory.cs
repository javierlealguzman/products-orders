using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsOrders.Domain.Payments;
using ProductsOrders.Infrastructure.Payments;

namespace ProductsOrders.IntegrationTests.Api;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public string PagoFacilOrderId { get; } = "123";
    public string CazaPagoOrderId { get; } = "456";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            var descriptors = services
                .Where(x => x.ServiceType == typeof(IExternalPaymentClient))
                .ToList();

            if (descriptors.Count > 0)
            {
                foreach (var descriptor in descriptors)
                    services.Remove(descriptor);
            }

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            

            var pagoFacilMock = new Mock<PagoFacilExternalPaymentClient>(httpClientFactoryMock.Object);
            pagoFacilMock
                .Setup(x => x.ProcessPaymentAsync(It.IsAny<Domain.DTOs.OrderRequestDto>()))
                .ReturnsAsync(PagoFacilOrderId);

            var cazaPagoMock = new Mock<CazaPagoExternalPaymentClient>(httpClientFactoryMock.Object);
            cazaPagoMock
                .Setup(x => x.ProcessPaymentAsync(It.IsAny<Domain.DTOs.OrderRequestDto>()))
                .ReturnsAsync(CazaPagoOrderId);

            services.AddSingleton<IExternalPaymentClient>(_ => pagoFacilMock.Object);
            services.AddSingleton<IExternalPaymentClient>(_ => cazaPagoMock.Object);
        });
    }
}