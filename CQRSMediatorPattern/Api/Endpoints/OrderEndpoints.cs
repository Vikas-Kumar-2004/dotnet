using CQRSMediatorPattern.Application.Orders.Command;
using CQRSMediatorPattern.Application.Orders.Dto;
using CQRSMediatorPattern.Application.Orders.Query;
using CQRSMediatorPattern.Abstractions;
using FluentValidation;

namespace CQRSMediatorPattern.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders").WithTags("Orders");

        // POST /orders — Create a new order (Command)
        group.MapPost("/", async (
            CreateOrderDto dto,
            IValidator<CreateOrderDto> validator,
            IDispatcher dispatcher) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await dispatcher.SendAsync<CreateOrderCommand, int>(new CreateOrderCommand(dto));
            return Results.Created($"/orders/{result}", result);
        })
        .WithName("CreateOrder")
        .WithSummary("Create a new order")
        .Produces<int>(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        // GET /orders — Get all orders (Query)
        group.MapGet("/", async (IDispatcher dispatcher) =>
        {
            var result = await dispatcher.QueryAsync<GetAllOrdersQuery, IEnumerable<OrderResponseDto>>(new GetAllOrdersQuery());
            return Results.Ok(result);
        })
        .WithName("GetAllOrders")
        .WithSummary("Get all orders")
        .Produces<IEnumerable<OrderResponseDto>>();

        // GET /orders/{id} — Get single order by ID (Query)
        group.MapGet("/{id:int}", async (int id, IDispatcher dispatcher) =>
        {
            var result = await dispatcher.QueryAsync<GetOrderQuery, OrderResponseDto?>(new GetOrderQuery(id));
            return result is null
                ? Results.NotFound(new { message = $"Order {id} not found" })
                : Results.Ok(result);
        })
        .WithName("GetOrderById")
        .WithSummary("Get a single order by ID")
        .Produces<OrderResponseDto>()
        .Produces(StatusCodes.Status404NotFound);
    }
}
