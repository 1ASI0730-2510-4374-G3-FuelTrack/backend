using FuelTracks.API.Orders.Domain.Model.Commands;
using FuelTracks.API.Orders.Interfaces.REST.Resources;

namespace FuelTracks.API.Orders.Interfaces.REST.Transform;

public static class CreateOrderCommandFromResourceAssembler
{
    public static CreateOrderCommand ToCommandFromResource(CreateOrderResource resource)
    {
        var items = resource.Items.Select(item =>
            new CreateOrderItemCommand(item.ProductId, item.Quantity, item.UnitPrice)).ToList();

        return new CreateOrderCommand(
            resource.ClientId,
            resource.ProviderId,
            resource.TerminalId,
            resource.TotalAmount,
            items
        );
    }
}
