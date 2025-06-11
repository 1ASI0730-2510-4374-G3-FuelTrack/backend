namespace FuelTracks.API.Orders.Interfaces.REST.Resources;

public record CreateOrderResource(
    int ClientId,
    int ProviderId,
    int TerminalId,
    decimal TotalAmount,
    List<CreateOrderItemResource> Items
);

public record CreateOrderItemResource(
    int ProductId,
    decimal Quantity,
    decimal UnitPrice
);
