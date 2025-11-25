using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductsOrders.Client.Models.PaymentOrder;
using ProductsOrders.Client.Services.PaymentOrder;

namespace ProductsOrders.Client.Shared;

public partial class OrderDetailModal : ComponentBase
{
    [Parameter] public int OrderId { get; set; }
    
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;

    [Inject] public PaymentOrderService PaymentOrderService { get; set; } = null!;

    public PaymentOrderDto PaymentOrder = new();

    protected override async Task OnInitializedAsync()
    {
        PaymentOrder = await PaymentOrderService.GetAsync(OrderId);
    }

    private void Close() => MudDialog.Cancel();

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
}