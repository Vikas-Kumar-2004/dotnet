using CQRSMediatorPattern.Application.Products.Dto;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatorPattern.Application.Products.Query;

public sealed record GetAllProductsQuery() : IQuery<IEnumerable<ProductResponseDto>>;

public sealed class GetAllProductsQueryHandler(AppDbContext dbContext) : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
{
    public async Task<IEnumerable<ProductResponseDto>> HandleAsync(GetAllProductsQuery request, CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Price = p.Price
            })
            .ToListAsync(cancellationToken);
    }
}
