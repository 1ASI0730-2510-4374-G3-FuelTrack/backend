namespace FuelTracks.API.Orders.Interfaces.REST.Resources;

public record OrderResource(
    int Id,
    int ClientId,
    int ProviderId,
    int TerminalId,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    List<ProductResource> Products,
    List<PaymentResource> Payments
);
