using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Program
{
    public static Player Player;
    static void Main(string[] args)
    {
        Console.Write(Color.RESET);
        Console.WriteLine($"Welcome to the {Color.FORE_LIGHT_GREEN}Text Adventure{Color.RESET}!");
        Console.Write("Please enter your name: " + Color.FORE_LIGHT_CYAN);

        string playerName = String.Empty;
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter && playerName.Length >= 2)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && playerName.Length > 0)
                {
                    playerName = playerName.Substring(0, playerName.Length - 1);
                    Console.Write("\b \b");
                }
                else if(Char.IsLetter(key.KeyChar))
                {
                    playerName += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }
        Console.Write(Color.RESET);
        Console.Clear();

        Player = new Player(playerName, 100, RoomDefinitions.StartingField);
        Player.CurrentRoom.Describe();

        while (true)
        {
            Console.Write(" > ");
        }
    }
}