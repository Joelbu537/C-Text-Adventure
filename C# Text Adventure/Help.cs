namespace TextAdventure;

using System;
using System.ComponentModel.Design;
using System.Diagnostics;

public static class Help
{
    private static List<HelpCommand> Commands = new List<HelpCommand>();
    private const string Title = " [ HELP PAGE ] ";
    private const string Deco = "*-";
    static Help()
    {
        Commands.Add(new HelpCommand(
            commandName: "status",
            description: "Prints a status message displaying the state your character is in"
            ));
        Commands.Add(new HelpCommand(
            commandName: "search",
            description: "Searches the current room/area for items"
            ));
        Commands.Add(new HelpCommand(
            commandName: "take",
            description: "Attempts to pick up the chosen item and add it to the inventory",
            commandAlias: ["pick", "pickup", "get", "grab"],
            parameters: ["Item", "all"]
            ));
        Commands.Add(new HelpCommand(
            commandName: "move",
            description: "Attempts to move the player in the given direction",
            commandAlias: ["go", "goto", "walk"],
            parameters: ["Direction"]
            ));
        Commands.Add(new HelpCommand(
            commandName: "inventory",
            description: "If not empty, opens the inventory.\n" +
            "Here, you can use, drop and inspect you items.",
            null
            ));
        Commands.Add(new HelpCommand(
            commandName: "describe",
            description: "Describes the current room.",
            commandAlias: ["look"]
        ));
        Commands.Add(new HelpCommand(
            commandName: "talk",
            description: "Talks to a specified NPC in the current room.",
            commandAlias: ["speak"],
            parameters: ["NPC Name"]
        ));
        Commands.Add(new HelpCommand(
            commandName: "clear",
            description: "Clears the console. Why would you do that though?",
            commandAlias: ["clr", "cls"]
        ));
        Commands.Add(new HelpCommand(
            commandName: "kys",
            description: "Either injures you or kills you instantly, depending on if you pass the \"now\" parameter.",
            parameters: ["nothing / now"]
        ));
        Commands.Add(new HelpCommand(
            commandName: "trade",
            commandAlias: ["buy", "deal"],
            description: "Attempts to buy the chosen item.",
            parameters: ["Item Number"]
        ));

        Commands.Sort();
    }

    public static void ListHelp()
    {
        Debug.WriteLine(Console.WindowWidth);
        int sideCount = (Console.WindowWidth - Title.Length) / 2 / Deco.Length;
        Console.ForegroundColor = ConsoleColor.White;

        for (int i = 0; i < sideCount; i++)
        {
            Console.Write(Deco);
        }
        Console.Write(Title);
        for (int i = 0; i < sideCount; i++)
        {
            Console.Write(Deco);
        }
        Console.ResetColor();
        Console.Write("\n\n");

        foreach (HelpCommand command in Commands)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"    {command.CommandName}");

            if (command.CommandAlias != null)
            {
                Console.Write(" (");
                Console.Write(string.Join(", ", command.CommandAlias));
                Console.Write(")");
            }
            if(command.Parameters != null)
            {
                Console.Write($"    <");
                Console.Write(string.Join(" / ", command.Parameters));
                Console.Write(">");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(command.Description);
            Console.WriteLine();
        }
        Console.WriteLine();
        for (int i = 0; i < Console.WindowWidth / Deco.Length; i++)
        {
            Console.Write(Deco);
        }
        Console.WriteLine();
    }
}

internal class HelpCommand : IComparable<HelpCommand>
{
    public string CommandName;
    public string[]? CommandAlias;
    public string[]? Parameters;
    public string Description;

    public HelpCommand(string commandName, string description, string[]? commandAlias = null, string[]? parameters = null)
    {
        CommandName = commandName;
        CommandAlias = commandAlias;
        Description = description;
        Parameters = parameters;
    }
    public int CompareTo(HelpCommand other)
    {
        if (other == null) return 1;

        return this.CommandName.CompareTo(other.CommandName);
    }
}