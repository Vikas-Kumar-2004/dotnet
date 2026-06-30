namespace CQRSMediatorPattern.Application.Products.Dto;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
