namespace ProductsOrders.Domain.Exceptions;

public class ExternalProviderException(string message) : Exception(message) { }