namespace ShoppingCartApp.Application.DTOs;

public class DisplayCartResultDto
{
    public List<ItemDto>? Items { get; set; }
    public double TotalAmount { get; set; }
    public int AppliedPromotionId { get; set; }
    public double TotalDiscount { get; set; }
}