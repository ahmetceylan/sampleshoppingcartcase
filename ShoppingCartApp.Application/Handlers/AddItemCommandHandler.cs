using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Handlers;

public class AddItemCommandHandler(Cart cart)
{
    public bool Handle(AddItemCommand command)
    {
        BaseItem item;

        // determine item type according to CategoryID
        if (command.CategoryId == 7889)
            item = new DigitalItem
            {
                Id = command.ItemId,
                CategoryId = command.CategoryId,
                SellerId = command.SellerId,
                Price = command.Price,
                Quantity = command.Quantity
            };
        else
            item = new DefaultItem
            {
                Id = command.ItemId,
                CategoryId = command.CategoryId,
                SellerId = command.SellerId,
                Price = command.Price,
                Quantity = command.Quantity
            };
        return cart.AddItem(item);
    }
}