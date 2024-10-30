using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Handlers;

public class RemoveItemCommandHandler(Cart cart)
{
    public bool Handle(RemoveItemCommand command)
    {
        var item = cart.Items.FirstOrDefault(i => i.Id == command.ItemId);

        if (item != null) return cart.RemoveItem(item.Id);
        Console.WriteLine("Remove Item is failed! itemId: " + item?.Id);
        // return false if the item doesn't exist
        return false;
    }
}