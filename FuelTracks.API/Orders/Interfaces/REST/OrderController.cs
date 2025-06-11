using FuelTracks.API.Orders.Domain.Model.Queries;
using FuelTracks.API.Orders.Domain.Model.Commands;
using FuelTracks.API.Orders.Domain.Services;
using FuelTracks.API.Orders.Interfaces.REST.Resources;
using FuelTracks.API.Orders.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FuelTracks.API.Orders.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Orders management")]
public class OrdersController : ControllerBase
{
    private readonly IOrderCommandService _orderCommandService;
    private readonly IOrderQueryService _orderQueryService;

    public OrdersController(IOrderCommandService orderCommandService, IOrderQueryService orderQueryService)
    {
        _orderCommandService = orderCommandService;
        _orderQueryService = orderQueryService;
    }

    /// <returns>List of all orders</returns>
    /// <response code="200">Returns list of orders</response>
    [HttpGet]
    [SwaggerOperation(Summary = "Get all orders", Description = "Retrieves all orders in the system")]
    [SwaggerResponse(200, "Orders retrieved successfully", typeof(IEnumerable<OrderResource>))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<OrderResource>>> GetAllOrders()
    {
        var getAllOrdersQuery = new GetAllOrdersQuery();
        var orders = await _orderQueryService.Handle(getAllOrdersQuery);
        var resources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <param name="id">Order ID</param>
    /// <returns>Order details</returns>
    /// <response code="200">Returns the order</response>
    /// <response code="404">Order not found</response>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get order by ID", Description = "Retrieves a specific order by its ID")]
    [SwaggerResponse(200, "Order found", typeof(OrderResource))]
    [SwaggerResponse(404, "Order not found")]
    [Produces("application/json")]
    public async Task<ActionResult<OrderResource>> GetOrderById([FromRoute] int id)
    {
        var getOrderByIdQuery = new GetOrderByIdQuery(id);
        var order = await _orderQueryService.Handle(getOrderByIdQuery);

        if (order == null)
            return NotFound($"Order with ID {id} not found");

        var resource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return Ok(resource);
    }

    /// <param name="resource">Order creation data</param>
    /// <returns>Created order</returns>
    /// <response code="201">Order created</response>
    /// <response code="400">Invalid order data</response>
    [HttpPost]
    [SwaggerOperation(Summary = "Create new order", Description = "Creates a new order in the system")]
    [SwaggerResponse(201, "Order created successfully", typeof(OrderResource))]
    [SwaggerResponse(400, "Invalid order data")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<OrderResource>> CreateOrder([FromBody] CreateOrderResource resource)
    {
        var createOrderCommand = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var order = await _orderCommandService.Handle(createOrderCommand);

        if (order == null)
            return BadRequest("Failed to create order");

        var orderResource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderResource);
    }

    /// <param name="id">Order ID</param>
    /// <param name="resource">Status update data</param>
    /// <returns>Updated order</returns>
    /// <response code="200">Order status updated</response>
    /// <response code="404">Order not found</response>
    /// <response code="400">Invalid status data</response>
    [HttpPut("{id:int}/status")]
    [SwaggerOperation(Summary = "Update order status", Description = "Updates the status of an existing order")]
    [SwaggerResponse(200, "Order status updated successfully", typeof(OrderResource))]
    [SwaggerResponse(404, "Order not found")]
    [SwaggerResponse(400, "Invalid status data")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<OrderResource>> UpdateOrderStatus(
        [FromRoute] int id,
        [FromBody] UpdateOrderStatusResource resource)
    {
        var updateOrderStatusCommand = new UpdateOrderStatusCommand(id, resource.Status);
        var order = await _orderCommandService.Handle(updateOrderStatusCommand);

        if (order == null)
            return NotFound($"Order with ID {id} not found");

        var orderResource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return Ok(orderResource);
    }


    /// <param name="userId">User ID</param>
    /// <returns>List of orders</returns>
    /// <response code="200">Returns the user orders</response>
    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(Summary = "Get orders by user", Description = "Retrieves all orders for a specific user")]
    [SwaggerResponse(200, "Orders retrieved successfully", typeof(IEnumerable<OrderResource>))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<OrderResource>>> GetOrdersByUser([FromRoute] int userId)
    {
        var getOrdersByUserQuery = new GetOrdersByUserQuery(userId);
        var orders = await _orderQueryService.Handle(getOrdersByUserQuery);
        var resources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Get orders by status
    /// </summary>
    /// <param name="status">Order status</param>
    /// <returns>List of orders with the specified status</returns>
    /// <response code="200">Returns orders with the specified status</response>
    [HttpGet("status/{status}")]
    [SwaggerOperation(Summary = "Get orders by status", Description = "Retrieves all orders with a specific status")]
    [SwaggerResponse(200, "Orders retrieved successfully", typeof(IEnumerable<OrderResource>))]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<OrderResource>>> GetOrdersByStatus([FromRoute] string status)
    {
        var getOrdersByStatusQuery = new GetOrdersByStatusQuery(status);
        var orders = await _orderQueryService.Handle(getOrdersByStatusQuery);
        var resources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
