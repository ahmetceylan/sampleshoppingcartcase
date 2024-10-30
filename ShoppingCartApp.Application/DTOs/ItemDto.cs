namespace ShoppingCartApp.Application.DTOs;

public class ItemDto
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}