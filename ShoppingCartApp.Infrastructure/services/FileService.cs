using Newtonsoft.Json;
using ShoppingCartApp.Infrastructure.models;

namespace ShoppingCartApp.Infrastructure.services;

public class FileService
{
    public List<BaseCommand>? ReadCommands(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Input file not found at path: {filePath}");

        var fileContent = File.ReadAllText(filePath);

        // parse json string according to command types
        //List<BaseCommand>? commands = JsonConvert.DeserializeObject<List<BaseCommand>>(fileContent);

        var jsonCommands = new List<BaseCommand>();
        var commands = JsonConvert.DeserializeObject<List<dynamic>>(fileContent);
        foreach (var command in commands)
        {
            Console.WriteLine(command.command);
            if (command.command == "addItem")
            {
                Console.WriteLine("command.ToObject<AddItemJsonCommand>() - " +
                                  command.ToObject<AddItemJsonCommand>().Payload.CategoryId);
                jsonCommands.Add(command.ToObject<AddItemJsonCommand>());
            }
            else if (command.command == "addVasItemToItem")
            {
                jsonCommands.Add(command.ToObject<AddVasItemToItemJsonCommand>());
            }
            else if (command.command == "removeItem")
            {
                jsonCommands.Add(command.ToObject<RemoveItemJsonCommand>());
            }
            else if (command.command == "resetCart")
            {
                jsonCommands.Add(command.ToObject<ResetCartJsonCommand>());
            }
            else if (command.command == "displayCart")
            {
                jsonCommands.Add(command.ToObject<DisplayCartJsonCommand>());
            }
        }


        return jsonCommands;
    }

    public string ReadFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Input file not found at path: {filePath}");

        return File.ReadAllText(filePath);
    }

    public void WriteFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    public void AppendFile(string filePath, string content)
    {
        File.AppendAllText(filePath, content);
    }
}