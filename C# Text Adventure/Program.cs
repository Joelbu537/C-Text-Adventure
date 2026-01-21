using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Program
{
    public static Player Player;
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
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

        Player = new Player(playerName, 100, 20, RoomDefinitions.StartingField);
        RoomDefinitions.InitRooms();
        Player.CurrentRoom = RoomDefinitions.StartingField;
        Player.CurrentRoom.Describe();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n > ");
            Console.ForegroundColor = ConsoleColor.White;
            string inputRaw = String.Empty;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter && inputRaw.Length >= 2)
                    {
                        break;
                    }
                    else if (key.Key == ConsoleKey.Backspace && inputRaw.Length > 0)
                    {
                        inputRaw = inputRaw.Substring(0, inputRaw.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (Char.IsAsciiLetterOrDigit(key.KeyChar) || key.KeyChar == ' ')
                    {
                        inputRaw += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
            }
            Console.Write(Color.RESET);
            string[] input = inputRaw.Trim().ToLower().Split(' ');

            Console.Clear();
            try
            {
                switch (input[0])
                {
                    case "status":
                        Player.Status();
                        break;
                    case "help":
                        Help.ListHelp();
                        break;
                    case "search":
                        Player.CurrentRoom.Search();
                        break;
                    case "pick":
                    case "pickup":
                    case "get":
                    case "take":
                        for (int i = 0; i < Player.CurrentRoom.Inventory.Count; i++)
                        {
                            if (Player.CurrentRoom.Inventory[i].RawName.ToLower() == input[1].ToLower())
                            {
                                Player.Inventory.Add(Player.CurrentRoom.Inventory[i]);
                                Console.WriteLine(Player.Name + " picked up " + Player.CurrentRoom.Inventory[i].Name +
                                                  Color.RESET);
                                Player.CurrentRoom.Inventory.RemoveAt(i);
                                break;
                            }
                        }
                        break;
                    case "move":
                    case "go":
                    case "walk":
                        bool state = Enum.TryParse(input[1].ToLower(), out RoomDirection direction);
                        if (!state)
                        {
                            throw new SyntaxErrorException();
                        }

                        Room targetRoom = Player.CurrentRoom.ConnectedRooms[(int)direction];
                        if (targetRoom != null)
                        {
                            if (!targetRoom.IsUnlocked)
                            {
                                Console.WriteLine("The way is blocked, and " + Program.Player.Name + "can't seem to find a different way going into that direction.");
                                break;
                            }
                            Player.CurrentRoom = Player.CurrentRoom.ConnectedRooms[(int)direction];
                            Player.CurrentRoom.Describe();
                            break;
                        }
                        Console.WriteLine(Player.Name + Color.FORE_LIGHT_RED + " cannot go into that direction!");
                        break;
                    default:
                        throw new SyntaxErrorException();
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new SyntaxErrorException();
            }
            catch (SyntaxErrorException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Syntax Error or missing parameters!");
                Console.ResetColor();
                Console.WriteLine("To see a list of all available commands, use \"help\"");
            }
            catch (ItemTooHeavyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}