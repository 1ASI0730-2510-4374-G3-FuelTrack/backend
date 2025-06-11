using FuelTracks.API.Orders.Domain.Model.Aggregates;
using FuelTracks.API.Orders.Domain.Model.Commands;
using FuelTracks.API.Orders.Domain.Repositories;
using FuelTracks.API.Orders.Domain.Services;
using FuelTracks.API.Shared.Domain.Repositories;

namespace FuelTracks.API.Orders.Application.Internal.CommandServices;

public class OrderCommandService(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : IOrderCommandService
{
    /// <inheritdoc />
    public async Task<Order?> Handle(CreateOrderCommand command)
    {
        var order = new Order(command);

        try
        {
            await orderRepository.AddAsync(order);
            await unitOfWork.CompleteAsync();
            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating order: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Order?> Handle(UpdateOrderStatusCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order == null) return null;

        order.UpdateStatus(command.Status);

        try
        {
            orderRepository.Update(order);
            await unitOfWork.CompleteAsync();
            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating order status: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Order?> Handle(AddPaymentCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order == null) return null;

        var payment = new Payment(command);
        order.AddPayment(payment);

        try
        {
            orderRepository.Update(order);
            await unitOfWork.CompleteAsync();
            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding payment: {e.Message}");
            return null;
        }
    }
}
