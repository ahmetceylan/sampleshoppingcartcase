namespace ShoppingCartApp.Infrastructure.models;

public class AddItemJsonCommand : BaseCommand
{
    public AddItemJsonPayload Payload { get; set; }
}

public class AddItemJsonPayload
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}