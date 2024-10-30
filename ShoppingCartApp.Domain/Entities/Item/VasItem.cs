namespace ShoppingCartApp.Domain.Entities.Item;

public class VasItem : BaseItem
{
    public int VasItemId { get; set; }

    public override bool Validate()
    {
        return SellerId == 5003 && CategoryId == 3242;
    }
}