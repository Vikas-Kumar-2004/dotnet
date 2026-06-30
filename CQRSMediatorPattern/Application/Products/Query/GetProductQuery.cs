using CQRSMediatorPattern.Application.Products.Dto;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;

namespace CQRSMediatorPattern.Application.Products.Query;

public sealed record GetProductQuery(int Id) : IQuery<ProductResponseDto?>;

public sealed class GetProductQueryHandler(AppDbContext dbContext) : IQueryHandler<GetProductQuery, ProductResponseDto?>
{
    public async Task<ProductResponseDto?> HandleAsync(GetProductQuery request, CancellationToken cancellationToken = default)
    {
        var product = await dbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);
        if (product is null) return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Barcode = product.Barcode,
            Price = product.Price
        };
    }
}
