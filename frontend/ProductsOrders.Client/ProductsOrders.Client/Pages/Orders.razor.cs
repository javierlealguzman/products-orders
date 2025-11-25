using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductsOrders.Client.Models.PaymentOrder;
using ProductsOrders.Client.Services.PaymentOrder;
using ProductsOrders.Client.Shared;

namespace ProductsOrders.Client.Pages;

public partial class Orders : ComponentBase
{
    public List<PaymentOrderDto> PaymentOrders = [];

    public bool Loading = true;
    [Inject] public PaymentOrderService PaymentOrderService { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    [Inject] public IDialogService DialogService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await GetPaymentOrdersAsync();
        Loading = false;
    }

    private async Task GetPaymentOrdersAsync()
    {
        Loading = true;
        PaymentOrders = await PaymentOrderService.GetAsync();
        Loading = false;
    }

    public static Color GetOrderStatusColor(string status)
    {
        return status switch
        {
            "Pending" => Color.Warning,
            "Cancelled" => Color.Error,
            "Completed" => Color.Success,
            _ => Color.Info
        };
    }

    async Task CancelOrder(int id)
    {
        try
        {
            await PaymentOrderService.CancelAsync(id);

            Snackbar.Add("Order cancelled successfully", Severity.Success);

            await GetPaymentOrdersAsync();
        }
        catch (Exception)
        {
            Snackbar.Add("Unable to delete order", Severity.Error);
        }
    }

    async Task PayOrder(int id)
    {
        try
        {
            await PaymentOrderService.PayAsync(id);

            Snackbar.Add("Order paid successfully", Severity.Success);

            await GetPaymentOrdersAsync();
        }
        catch (Exception)
        {
            Snackbar.Add("Unable to pay order", Severity.Error);
        }
    }

    async Task OpenOrderDetailModal(int id)
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium
        };

        var parameters = new DialogParameters<OrderDetailModal>
        {
            { x => x.OrderId, id }
        };

        await DialogService.ShowAsync<OrderDetailModal>("Order Details", parameters, options);
    }
}