using CQRSMediatorPattern.Application.Orders.Dto;
using CQRSMediatorPattern.Domain.Entities;
using CQRSMediatorPattern.Persistence.DbContexts;
using CQRSMediatorPattern.Abstractions;
using CQRSMediatorPattern.Handlers;

namespace CQRSMediatorPattern.Application.Orders.Command;

public sealed class CreateOrderCommand(CreateOrderDto orderDto) : ICommand<int>// "When this command is executed, it will return an integer.: order.Id
{
    public CreateOrderDto OrderDto { get; } = orderDto;

    public sealed class Handler(AppDbContext dbContext) : ICommandHandler<CreateOrderCommand, int>
    {
        public async Task<int> HandleAsync(CreateOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                ProductName = request.OrderDto.ProductName,
                Quantity = request.OrderDto.Quantity,
                Price = request.OrderDto.Price,
                CustomerName = request.OrderDto.CustomerName,
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return order.Id;
        }
    }
}

/*
 
1) In old C# syntax, the constructor is inside the class:
public sealed class CreateOrderCommand : IRequest<int> 
{
    public CreateOrderCommand(CreateOrderDto orderDto) =
    {
    }
}

 Class = CreateOrderCommand
Constructor = CreateOrderCommand(CreateOrderDto orderDto)
-----------------------------------------------------------------------------------------------------------
2) What is : IRequest<int>? 
"This command implements the MediatR interface IRequest<int>."

The int means the handler will return an integer.
Eg" public class Handler : IRequestHandler<CreateOrderCommand, int>
{
    public Task<int> Handle(CreateOrderCommand request,
                            CancellationToken cancellationToken)
    {
        return Task.FromResult(101); // Order Id
    }
}
--------------------------------------------------------------------------------------------------

3) public CreateOrderDto OrderDto { get; } its liek geetter :

| New C#             | Old C#                    | C++                |
| ------------------ | ------------------------- | ------------------ |
| `Student(int age)` | `public Student(int age)` | `Student(int age)` |
| `Age { get; }`     | `public int Age { get; }` | `getAge()`         |
| `= age`            | `Age = age;`              | `this->age = age;` |

 
 
 
 
 
 
 
 
 
 
 
 
 
 */