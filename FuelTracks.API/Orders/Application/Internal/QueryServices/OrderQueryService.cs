using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Domain.Model.Queries;
using FuelTracks.API.Orders.Domain.Repositories;
using FuelTracks.API.Orders.Domain.Services;

namespace FuelTracks.API.Orders.Application.Internal.QueryServices;

public class OrderQueryService(IOrderRepository orderRepository) : IOrderQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query)
    {
        return await orderRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Order?> Handle(GetOrderByIdQuery query)
    {
        return await orderRepository.FindByIdAsync(query.Id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetOrdersByUserQuery query)
    {
        return await orderRepository.FindByUserIdAsync(query.UserId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> Handle(GetOrdersByStatusQuery query)
    {
        return await orderRepository.FindByStatusAsync(query.Status);
    }
}
