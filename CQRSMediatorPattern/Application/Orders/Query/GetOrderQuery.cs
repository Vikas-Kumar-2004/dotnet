using CQRSMediatorPattern.Application.Orders.Dto;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;

namespace CQRSMediatorPattern.Application.Orders.Query;

public sealed class GetOrderQuery(int id) : IQuery<OrderResponseDto?>
{
    public int Id { get; } = id;

    public sealed class Handler(AppDbContext dbContext) : IQueryHandler<GetOrderQuery, OrderResponseDto?>
    {
        public async Task<OrderResponseDto?> HandleAsync(GetOrderQuery request, CancellationToken cancellationToken = default)
        {
            var order = await dbContext.Orders.FindAsync(new object[] { request.Id }, cancellationToken);
            if (order == null) return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price,
                CustomerName = order.CustomerName,
            };
        }
    }
}
