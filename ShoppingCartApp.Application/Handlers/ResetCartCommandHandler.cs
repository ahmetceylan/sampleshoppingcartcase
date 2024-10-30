using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Handlers;

public class ResetCartCommandHandler(Cart cart)
{
    public bool Handle(ResetCartCommand command)
    {
        cart.ResetCart(); // removes all items from cart
        return true;
    }
}