namespace ShoppingCartApp.Domain.Entities.Item;

public class DigitalItem : BaseItem
{
    public override bool Validate()
    {
        // max 5 Digital items can be added
        // and category id must be 7889
        return CategoryId == 7889 && Quantity <= 5;
    }
}