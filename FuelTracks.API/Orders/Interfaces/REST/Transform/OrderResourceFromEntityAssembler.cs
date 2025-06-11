using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Interfaces.REST.Resources;

namespace FuelTracks.API.Orders.Interfaces.REST.Transform;

public static class OrderResourceFromEntityAssembler
{
    public static OrderResource ToResourceFromEntity(Order entity)
    {
        var products = entity.Products?.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity).ToList() ?? new List<ProductResource>();
        var payments = entity.Payments?.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity).ToList() ?? new List<PaymentResource>();

        return new OrderResource(
            entity.Id,
            entity.ClientId,
            entity.ProviderId,
            entity.TerminalId,
            entity.TotalAmount,
            entity.Status,
            entity.CreatedAt,
            products,
            payments
        );
    }
}
