using Microsoft.AspNetCore.Components;
using ProductsOrders.Client.Models.PaymentOrder;
using ProductsOrders.Client.Services.PaymentOrder;

namespace ProductsOrders.Client.Pages;

public partial class Orders : ComponentBase
{
    public List<PaymentOrderDto> PaymentOrders = [];

    public bool Loading = true;
    [Inject] public PaymentOrderService PaymentOrderService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        PaymentOrders = await PaymentOrderService.GetAsync();
        Loading = false;
    }
}