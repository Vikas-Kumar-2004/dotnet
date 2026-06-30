using CQRSMediatorPattern.Application.Orders.Dto;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatorPattern.Application.Orders.Query;

public sealed class GetAllOrdersQuery : IQuery<IEnumerable<OrderResponseDto>>
{
    public sealed class Handler(AppDbContext dbContext) : IQueryHandler<GetAllOrdersQuery, IEnumerable<OrderResponseDto>>
    {
        public async Task<IEnumerable<OrderResponseDto>> HandleAsync(GetAllOrdersQuery request, CancellationToken cancellationToken = default)
        {
            return await dbContext.Orders
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    ProductName = o.ProductName,
                    Quantity = o.Quantity,
                    Price = o.Price,
                    CustomerName=o.CustomerName
                }).ToListAsync(cancellationToken);
        }
    }
}
