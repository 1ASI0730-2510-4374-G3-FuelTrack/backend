using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Domain.Model.Commands;

namespace FuelTracks.API.Orders.Domain.Services;

public interface IOrderCommandService
{
    Task<Order?> Handle(CreateOrderCommand command);
    Task<Order?> Handle(UpdateOrderStatusCommand command);
    Task<Order?> Handle(AddPaymentCommand command);
}
