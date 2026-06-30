using FluentValidation;
using CQRSMediatorPattern.Application.Products.Dto;

namespace CQRSMediatorPattern.Application.Products.Validator;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Barcode).NotEmpty().WithMessage("Barcode is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
