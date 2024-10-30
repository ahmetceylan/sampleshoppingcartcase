using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Application.DTOs;
using ShoppingCartApp.Application.Handlers;
using ShoppingCartApp.Domain.Entities;

namespace ShoppingCartApp.Application.Services;

public class CartService
{
    private readonly Cart _cart;
    private readonly PromotionService _promotionService;

    public CartService(Cart cart)
    {
        _cart = cart;
        _promotionService = new PromotionService();
    }

    public bool AddItem(AddItemCommand command)
    {
        var handler = new AddItemCommandHandler(_cart);
        return handler.Handle(command);
    }

    public bool AddVasItemToItem(AddVasItemToItemCommand command)
    {
        var handler = new AddVasItemToItemCommandHandler(_cart);
        return handler.Handle(command);
    }

    public bool RemoveItem(RemoveItemCommand command)
    {
        var handler = new RemoveItemCommandHandler(_cart);
        return handler.Handle(command);
    }

    public bool ResetCart(ResetCartCommand command)
    {
        var handler = new ResetCartCommandHandler(_cart);
        return handler.Handle(command);
    }

    public DisplayCartResultDto DisplayCart(DisplayCartCommand command)
    {
        _promotionService.ApplyBestPromotion(_cart);
        var handler = new DisplayCartCommandHandler(_cart);
        return handler.Handle(command);
    }
    // TODO add handlers for other commands
}