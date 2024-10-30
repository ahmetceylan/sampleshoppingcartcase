namespace ShoppingCartApp.Application.Commands;

public class AddVasItemToItemCommand
{
    public int ItemId { get; set; }
    public int VasItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}