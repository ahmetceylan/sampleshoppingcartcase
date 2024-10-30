namespace ShoppingCartApp.Infrastructure.models;

public class RemoveItemJsonCommand : BaseCommand
{
    public RemoveItemJsonPayload Payload { get; set; }
}

public class RemoveItemJsonPayload
{
    public int ItemId { get; set; }
}