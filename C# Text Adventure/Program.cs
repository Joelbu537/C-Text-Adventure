namespace TextAdventure;

using System.Data;
using System.Diagnostics;
public static class Program
{
    public static Player? Player;
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
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
                else if(char.IsLetter(key.KeyChar))
                {
                    playerName += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }
        Console.Write(Color.RESET);
        Console.Clear();

        Player = new Player(playerName, 100, 20, RoomDefinitions.StartingField);
        NPCDefinitions.InitNPCs();
        RoomDefinitions.InitRooms();
        Player.CurrentRoom = RoomDefinitions.StartingField;
        Player.CurrentRoom.Describe();

        while (true)
        {
                if (Player.Hp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine(new string('\n', Console.WindowHeight / 2 - 2));
                    string deathMessage = $"{Color.FORE_LIGHT_RED}{Player!.Name.Clean()} met their final fate!";
                    int windowWidth = deathMessage.Clean().Length;

                    Boxing.WriteLineCentered(Boxing.WindowCeiling(windowWidth, Color.FORE_LIGHT_RED));
                    Boxing.WriteLineCentered(Boxing.WindowWall(deathMessage));
                    System.Diagnostics.Debug.WriteLine($"Death Message Length: {deathMessage.Clean().Length}, Window Width: {windowWidth}");
                    StreamWriter writer = File.CreateText("deathlog.txt");
                    writer.WriteLine($"Death Message Length: {deathMessage.Clean().Length}|{deathMessage.Length}, Window Width: {windowWidth}");
                    writer.Close();
                    Boxing.WriteLineCentered(Boxing.WindowWall(Boxing.Center((deathMessage.Clean().Length < 27) 
                    ? $"{Color.FORE_LIGHT_RED}Press any key..." : $"{Color.FORE_LIGHT_RED}Press any key to continue...", windowWidth), windowWidth));
                    Boxing.WriteLineCentered(Boxing.WindowFloor(windowWidth, Color.FORE_LIGHT_RED));
                    Console.ReadKey();
                    return;
                }


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
                    if (key.Key == ConsoleKey.Backspace && inputRaw.Length > 0)
                    {
                        inputRaw = inputRaw.Substring(0, inputRaw.Length - 1);
                        Console.Write("\b \b");
                    }
                    if (char.IsAsciiLetterOrDigit(key.KeyChar) || key.KeyChar == ' ')
                    {
                        inputRaw += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
            }
            Console.Write(Color.RESET);
            string[] input = inputRaw.Trim().ToLower().Split(' ');

            InputHandling(input);
        }
    }

    private static void InputHandling(string[] input)
    {
        Console.Clear();
        try
        {
                switch (input[0])
                {
                    case "clr":
                    case "cls":
                    case "clear":
                        Console.Clear();
                        break;
                    case "status":
                        Player!.Status();
                        break;
                    case "inventory":
                        if (Player!.Inventory.Count == 0)
                        {
                            Console.WriteLine($"{Player!.Name}'s {Color.FORE_WHITE}inventory{Color.RESET} is {Color.FORE_LIGHT_RED}empty{Color.RESET}!");
                            break;
                        }
                        InventoryDisplay.InventoryLoop();
                        break;
                    case "help":
                        Help.ListHelp();
                        break;
                    case "search":
                        Player?.CurrentRoom.Search();
                        break;
                    case "pick":
                    case "grab":
                    case "pickup":
                    case "get":
                    case "take":
                        if(input.Length < 2) throw new SyntaxErrorException();
                        if(input.Length > 1 && input[1].ToLower() == "all")
                        {
                            for (int i = Player?.CurrentRoom.Inventory.Count - 1 ?? 0; i >= 0; i--)
                            {
                                try
                                {
                                    Player!.Inventory.Add(Player!.CurrentRoom.Inventory[i]);
                                    Console.WriteLine(Player!.Name + " picked up " + Player!.CurrentRoom.Inventory[i].Name + Color.RESET);
                                    Player!.CurrentRoom.Inventory.RemoveAt(i);
                                }
                                catch (ItemTooHeavyException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }       
                            }
                            return;
                        }
                        for (int i = 0; i < Player!.CurrentRoom.Inventory.Count; i++)
                        {
                            if (Player!.CurrentRoom.Inventory[i].Name.Clean().ToLower() == String.Join(' ', input[1..]).ToLower())
                            {
                                try
                                {
                                    Player!.Inventory.Add(Player!.CurrentRoom.Inventory[i]);
                                    Console.WriteLine(Player!.Name + " picked up " + Player!.CurrentRoom.Inventory[i].Name + Color.RESET);
                                    Player!.CurrentRoom.Inventory.RemoveAt(i);
                                }
                                catch(ItemTooHeavyException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                return;
                            }
                        }
                        Console.WriteLine(Player!.Name + " could not find anything named \"" + Color.FORE_CYAN + String.Join(' ', input[1..]) + Color.RESET + "\"");
                        break;
                    case "move":
                    case "go":
                    case "walk":
                        var state = Enum.TryParse(input[1].ToLower(), out RoomDirection direction);
                        if (!state)
                        {
                            throw new SyntaxErrorException();
                        }

                        Room? targetRoom = Player?.CurrentRoom.ConnectedRooms[(int)direction];
                        if (targetRoom != null)
                        {
                            if (!targetRoom.IsUnlocked)
                            {
                                Console.WriteLine("The path is blocked, and " + Player?.Name + "can't seem to find a different way around.");
                                break;
                            }
                            Player?.CurrentRoom = Player.CurrentRoom.ConnectedRooms[(int)direction]!;
                            Player?.CurrentRoom.Describe();
                            break;
                        }
                        Console.WriteLine(Player?.Name + Color.FORE_LIGHT_RED + " cannot go into that direction!");
                        break;
                    case "describe":
                    case "look":
                        Player?.CurrentRoom.Describe();
                        break;
                    case "kys":
                        if(input.Length > 1 && input[1] == "now")
                        {
                            Player?.Damage(short.MaxValue);
                            break;
                        }
                        Player?.Damage(14);
                        break;
                    case "talk":
                    case "speak":
                    case "trade":
                        foreach(NPC? npc in Player!.CurrentRoom.NPCs!)
                        {
                            if(npc is not FriendlyNPC) continue;
                            if (npc.Name.Clean().ToLower() == string.Join(' ', input[1..]).ToLower())
                            {
                                FriendlyNPC? trader = npc as FriendlyNPC;
                                trader?.Trade();
                                return;
                            }
                            
                        }
                        break;
                    default:
                        throw new SyntaxErrorException();
                }
        }
        catch (IndexOutOfRangeException)
        {
            SyntaxError();
        }
        catch (SyntaxErrorException)
        {
            SyntaxError();
        }
        catch (ItemTooHeavyException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void SyntaxError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Syntax Error or missing parameters!");
        Console.ResetColor();
        Console.WriteLine("To see a list of all available commands, use \"help\"");
    }
}