namespace ShoppingCartApp.Infrastructure.models;

public class ResetCartJsonCommand : BaseCommand
{
    public string Payload { get; } = "";
}