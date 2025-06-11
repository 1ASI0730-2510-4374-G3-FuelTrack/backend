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
