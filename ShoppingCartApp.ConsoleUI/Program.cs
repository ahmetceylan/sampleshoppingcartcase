// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Newtonsoft.Json;
using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Application.Services;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Infrastructure.services;

Console.WriteLine(" Shopping Cart Application is starting...");
var _fileService = new FileService();

var cart = new Cart();
var cartService = new CartService(cart);
var rootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

var outputFilePath = rootDir + "/output.json";
var inputFilePath = rootDir + "/input.json";

try
{
    // read commands from file
    var input = _fileService.ReadFile(inputFilePath);
    var commands = JsonConvert.DeserializeObject<List<dynamic>>(input);

    if (commands != null)
    {
        foreach (var command in commands)
            if (command.command == "addItem")
            {
                var addItemCommand = JsonConvert.DeserializeObject<AddItemCommand>(command.payload.ToString());
                var result = cartService.AddItem(addItemCommand);
                var output = new
                {
                    result = result,
                    message = result ? "Item added successfully" : "Failed to add item"
                };
                _fileService.AppendFile(outputFilePath, JsonConvert.SerializeObject(output) + Environment.NewLine);
            }
            else if (command.command == "addVasItemToItem")
            {
                var addVasItemCommand =
                    JsonConvert.DeserializeObject<AddVasItemToItemCommand>(command.payload.ToString());
                var result = cartService.AddVasItemToItem(addVasItemCommand);
                var output = new
                {
                    result = result,
                    message = result ? "Vas item added successfully" : "Failed to add vas item"
                };
                _fileService.AppendFile(outputFilePath, JsonConvert.SerializeObject(output) + Environment.NewLine);
            }
            else if (command.command == "removeItem")
            {
                var removeItemCommand = JsonConvert.DeserializeObject<RemoveItemCommand>(command.payload.ToString());
                var result = cartService.RemoveItem(removeItemCommand);
                var output = new
                {
                    result = result,
                    message = result ? "Item removed successfully" : "Failed to remove item from cart"
                };
                _fileService.AppendFile(outputFilePath, JsonConvert.SerializeObject(output) + Environment.NewLine);
            }
            else if (command.command == "resetCart")
            {
                var resetCartCommand = JsonConvert.DeserializeObject<ResetCartCommand>("");
                var result = cartService.ResetCart(resetCartCommand);
                var output = new
                {
                    result = result,
                    message = result ? "Cart reset successfully finished" : "Failed to reset cart"
                };
                _fileService.AppendFile(outputFilePath, JsonConvert.SerializeObject(output) + Environment.NewLine);
            }
            else if (command.command == "displayCart")
            {
                var displayCartCommand = JsonConvert.DeserializeObject<DisplayCartCommand>("");
                var result = cartService.DisplayCart(displayCartCommand);
                var output = new
                {
                    result = result.Items?.Count != 0 ? true : false,
                    message = result
                };
                _fileService.AppendFile(outputFilePath, JsonConvert.SerializeObject(output) + Environment.NewLine);
            }
    }
    else
    {
        Console.WriteLine("Commands could not be parsed from input file!");
    }

    Console.WriteLine("Operation completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}