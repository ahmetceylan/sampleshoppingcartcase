namespace ShoppingCartApp.Domain.Entities.Item;

public class DefaultItem : BaseItem
{
    private List<VasItem> _vasItems = new();

    public bool AddVasItem(VasItem vasItem)
    {
        // Check if the item already has 3 VasItems
        if (_vasItems.Count >= 3)
            throw new InvalidOperationException("Cannot add more than 3 VasItems to a DefaultItem.");

        // Check if the VasItem's price
        if (vasItem.Price > Price)
            throw new InvalidOperationException("VasItem's price cannot be higher than the DefaultItem's price.");

        // Add VasItem
        _vasItems.Add(vasItem);
        return true;
    }

    // returns the list of VasItems
    public List<VasItem> GetVasItems()
    {
        return _vasItems;
    }

    public override bool Validate()
    {
        // max 10 default items can be added
        return Quantity <= 10;
    }
}