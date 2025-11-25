using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductsOrders.Client.Models.Enums;
using ProductsOrders.Client.Models.PaymentOrder;
using ProductsOrders.Client.Models.Product;
using ProductsOrders.Client.Services.PaymentOrder;
using ProductsOrders.Client.Services.Product;
using ProductsOrders.Client.Shared;

namespace ProductsOrders.Client.Pages;

public partial class Products : ComponentBase
{
    public List<ProductDto> ProductsList = [];

    public HashSet<ProductDto> SelectedProducts = [];

    public bool Loading = true;

    [Inject] public ProductService ProductService { get; set; } = null!;

    [Inject] public PaymentOrderService PaymentOrderService { get; set; } = null!;

    [Inject] public IDialogService DialogService { get; set; } = null!;

    [Inject] public ISnackbar SnackBar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        ProductsList = await ProductService.GetProductsAsync();
        Loading = false;
    }

    private async Task OpenPaymentTypeModal()
    {
        if (SelectedProducts.Count == 0)
        {
            SnackBar.Add("At least one product must be selected", Severity.Info);
            return;
        }
        
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<PaymentTypeModal>("Title Dialog", options);

        var result = await dialog.Result;

        if (result != null && !result.Canceled)
        {
            var paymentType = (PaymentType) result.Data!;

            await CreatePaymentOrder(paymentType);
        }
    }

    private async Task CreatePaymentOrder(PaymentType paymentType)
    {
        try
        {
            var request = new PaymentOrderRequest
            {
                PaymentType = paymentType,
                Products = [.. SelectedProducts.Select(x => new ProductRequestDto
                {
                    Name = x.Name,
                    UnitPrice = x.UnitPrice
                })]
            };

            var paymentOrderDto = await PaymentOrderService.CreatePaymentOrderAsync(request);

            SelectedProducts = [];
            SnackBar.Add($"Order created successfully with order id {paymentOrderDto.OrderId}", Severity.Success);
        } catch (Exception ex)
        {
            SnackBar.Add(ex.Message, Severity.Error);
        }
    }
}