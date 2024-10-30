using Moq;
using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Application.Handlers;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Tests;

public class RemoveItemCommandHandlerTests
{
    private readonly RemoveItemCommandHandler _handler;
    private readonly Cart _cart;


    public RemoveItemCommandHandlerTests()
    {
        var defaultItem = new DefaultItem
        {
            Id = 1,
            CategoryId = 1001,
            SellerId = 5001,
            Price = 100,
            Quantity = 2
        };

        // Mock cart with one item
        _cart = new Cart();
        _cart.AddItem(defaultItem);
        _handler = new RemoveItemCommandHandler(_cart);
    }

    [Fact]
    public void Handle_ShouldRemoveItem_WhenItemExistsInCart()
    {
        var command = new RemoveItemCommand { ItemId = 1 };
        var result = _handler.Handle(command);

        // Assert
        Assert.True(result);
        Assert.Empty(_cart.Items);
    }

    [Fact]
    public void Handle_ShouldReturnFailure_WhenItemDoesNotExistInCart()
    {
        var command = new RemoveItemCommand { ItemId = 999 }; // Non-existing item
        var result = _handler.Handle(command);

        // Assert
        Assert.False(result);
    }
}