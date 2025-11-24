namespace ProductsOrders.Domain.Errors;

public class ErrorResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
}