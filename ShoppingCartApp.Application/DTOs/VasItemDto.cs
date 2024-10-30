namespace ShoppingCartApp.Application.DTOs;

public class VasItemDto
{
    public int VasItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}