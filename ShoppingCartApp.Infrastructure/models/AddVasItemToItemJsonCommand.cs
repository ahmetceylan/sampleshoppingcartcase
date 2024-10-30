namespace ShoppingCartApp.Infrastructure.models;

public class AddVasItemToItemJsonCommand : BaseCommand
{
    public AddVasItemToItemJsonPayload Payload { get; set; }
}

public class AddVasItemToItemJsonPayload
{
    public int ItemId { get; set; }
    public int vasItemId { get; set; }
    public int vasCategoryId { get; set; }
    public int vasSellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}