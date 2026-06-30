using CQRSMediatorPattern.Application.Products.Dto;
using CQRSMediatorPattern.Domain.Entities;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;

namespace CQRSMediatorPattern.Application.Products.Command;

public sealed record CreateProductCommand(CreateProductDto Dto) : ICommand<int>;

public sealed class CreateProductCommandHandler(AppDbContext dbContext) : ICommandHandler<CreateProductCommand, int>
{
    public async Task<int> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Name = request.Dto.Name,
            Barcode = request.Dto.Barcode,
            Price = request.Dto.Price
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
