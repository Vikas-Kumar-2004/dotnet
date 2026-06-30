using CQRSMediatorPattern.Application.Products.Command;
using CQRSMediatorPattern.Application.Products.Dto;
using CQRSMediatorPattern.Application.Products.Query;
using CQRSMediatorPattern.Abstractions;
using FluentValidation;

namespace CQRSMediatorPattern.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        // POST /products — Create a new product (Command)
        group.MapPost("/", async (
            CreateProductDto dto,
            IValidator<CreateProductDto> validator,
            IDispatcher dispatcher) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var result = await dispatcher.SendAsync<CreateProductCommand, int>(new CreateProductCommand(dto));
            return Results.Created($"/products/{result}", result);
        })
        .WithName("CreateProduct")
        .WithSummary("Create a new product")
        .Produces<int>(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        // GET /products — Get all products (Query)
        group.MapGet("/", async (IDispatcher dispatcher) =>
        {
            var result = await dispatcher.QueryAsync<GetAllProductsQuery, IEnumerable<ProductResponseDto>>(new GetAllProductsQuery());
            return Results.Ok(result);
        })
        .WithName("GetAllProducts")
        .WithSummary("Get all products")
        .Produces<IEnumerable<ProductResponseDto>>();

        // GET /products/{id} — Get single product by ID (Query)
        group.MapGet("/{id:int}", async (int id, IDispatcher dispatcher) =>
        {
            var result = await dispatcher.QueryAsync<GetProductQuery, ProductResponseDto?>(new GetProductQuery(id));
            return result is null
                ? Results.NotFound(new { message = $"Product {id} not found" })
                : Results.Ok(result);
        })
        .WithName("GetProductById")
        .WithSummary("Get a single product by ID")
        .Produces<ProductResponseDto>()
        .Produces(StatusCodes.Status404NotFound);
    }
}
