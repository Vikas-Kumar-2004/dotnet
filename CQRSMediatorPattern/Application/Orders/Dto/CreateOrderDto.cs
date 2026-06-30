namespace CQRSMediatorPattern.Application.Orders.Dto;

public class CreateOrderDto
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}
