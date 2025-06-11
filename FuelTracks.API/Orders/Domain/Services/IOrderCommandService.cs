using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Domain.Model.Commands;

namespace FuelTracks.API.Orders.Domain.Services;

public interface IOrderCommandService
{
    Task<Order?> Handle(CreateOrderCommand command);
    Task<Order?> Handle(UpdateOrderStatusCommand command);
    Task<Order?> Handle(AddPaymentCommand command);
}

// Orders/Domain/Services/IOrderQueryService.cs
using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Domain.Model.Queries;

namespace FuelTracks.API.Orders.Domain.Services;

public interface IOrderQueryService
{
    Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query);
    Task<Order?> Handle(GetOrderByIdQuery query);
    Task<IEnumerable<Order>> Handle(GetOrdersByUserQuery query);
    Task<IEnumerable<Order>> Handle(GetOrdersByStatusQuery query);
}
