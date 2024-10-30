using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Handlers;

public class AddVasItemToItemCommandHandler(Cart cart)
{
    public bool Handle(AddVasItemToItemCommand command)
    {
        var item = (DefaultItem)cart.Items.FirstOrDefault(i => i.Id == command.ItemId);

        if (item != null && command.VasCategoryId == 3242 && command.VasSellerId == 5003)
        {
            var vasItem = new VasItem
            {
                VasItemId = command.VasItemId,
                Id = command.ItemId,
                CategoryId = command.VasCategoryId,
                SellerId = command.VasSellerId,
                Price = command.Price,
                Quantity = command.Quantity
            };

            var isAdded = item.AddVasItem(vasItem); // add vasItem into the defaultItem
            if (isAdded)
                return cart.AddItem(vasItem);
            else
                return false;
        }

        // returns false if there is no item or VasItem can't be added
        return false;
    }
}