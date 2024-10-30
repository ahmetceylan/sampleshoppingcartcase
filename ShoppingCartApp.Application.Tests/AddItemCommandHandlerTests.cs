using Moq;
using FluentAssertions;
using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Application.Handlers;
using ShoppingCartApp.Domain.Entities;

namespace ShoppingCartApp.Application.Tests;

public class AddItemCommandHandlerTests
{
    private readonly Mock<Cart> _cartServiceMock;
    private readonly AddItemCommandHandler _handler;

    public AddItemCommandHandlerTests()
    {
        _cartServiceMock = new Mock<Cart>();
        _handler = new AddItemCommandHandler(_cartServiceMock.Object);
    }

    [Fact]
    public void Handle_ShouldReturnTrue_WhenItemIsAddedSuccessfully()
    {
        var addItemCommand = new AddItemCommand
        {
            ItemId = 1,
            CategoryId = 1001,
            SellerId = 5001,
            Price = 100,
            Quantity = 2
        };

        var result = _handler.Handle(addItemCommand);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Handle_ShouldReturnFalse_WhenAddItemFails()
    {
        var addItemCommand = new AddItemCommand
        {
            ItemId = 1,
            CategoryId = 1001,
            SellerId = 5001,
            Price = 10000000, // exceeds the price limit
            Quantity = 2
        };

        var result = _handler.Handle(addItemCommand);

        // Assert
        result.Should().BeFalse();
    }
}