using CQRSMediatorPattern.Application.Orders.Dto;
using FluentValidation;

namespace CQRSMediatorPattern.Application.Orders.Validator;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.ProductName)
      .NotEmpty().WithMessage("Product name is required.")
      .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.CustomerName)
    .NotEmpty().WithMessage("Customer name is required.")
    .MinimumLength(2).WithMessage("Customer name must be at least 2 characters.")
    .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");
    }
}
