namespace ShoppingCartApp.Domain.Entities.Item;

public abstract class BaseItem
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    // Every item type will have its own validation rule
    public abstract bool Validate();
}