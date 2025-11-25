using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductsOrders.Client.Models.Enums;

namespace ProductsOrders.Client.Shared;

public partial class PaymentTypeModal : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = null!;

    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    public PaymentType SelectedOption { get; set; }

    private void Cancel() => MudDialog.Cancel();

    private void Create()
    {
        MudDialog.Close(DialogResult.Ok(SelectedOption));
    }
}