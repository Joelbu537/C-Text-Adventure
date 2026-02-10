namespace TextAdventure;
using System.Data;
using System.Diagnostics;
using TextAdventure.NPCs;
public static class Program
{
    public delegate void Action();

    public static Player Player = new Player("InternalError", 0, 0, null!);
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Write(Color.RESET);
        Console.WriteLine("Source: https://github.com/Joelbu537/C-Text-Adventure\n");
        Console.WriteLine($"Welcome to the {Color.FORE_LIGHT_GREEN}Text Adventure{Color.RESET}!");
        Console.Write("Please enter your name: " + Color.FORE_LIGHT_CYAN);

        string playerName = string.Empty;
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
                else if (key.Key == ConsoleKey.Spacebar && playerName.Length != 0)
                {
                    playerName += ' ';
                    Console.Write(' ');
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

        Player = new Player(playerName.Trim(), 100, 10, RoomDefinitions.StartingField);
        RoomDefinitions.InitRooms();
        Player.CurrentRoom = RoomDefinitions.StartingField;
        Player.CurrentRoom.Describe();

        while (true)
        {
            if (Player.Hp <= 0)
            {
                Console.Clear();
                Console.WriteLine(new string('\n', Console.WindowHeight / 2 - 4));
                string deathMessage = $"{Color.FORE_LIGHT_RED}{Player!.Name.Clean()} met their final fate!";
                int windowWidth = deathMessage.Clean().Length;

                Boxing.WriteLineCentered(Boxing.WindowCeiling(windowWidth, Color.FORE_RED));
                Boxing.WriteLineCentered(Boxing.WindowWall(deathMessage, Color.FORE_RED));
                Boxing.WriteLineCentered(Boxing.WindowWall(Boxing.Center((deathMessage.Clean().Length < 27) 
                ? $"{Color.FORE_LIGHT_RED}Press any key..." : $"{Color.FORE_LIGHT_RED}Press any key to continue...", windowWidth), windowWidth, Color.FORE_RED));
                Boxing.WriteLineCentered(Boxing.WindowFloor(windowWidth, Color.FORE_RED));

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

            if(Player.CurrentRoom.NPCs != null)
            {
                Console.WriteLine();
                foreach(NPC npc in Player.CurrentRoom.NPCs)
                {
                    if(npc is HostileNPC enemy)
                        Console.WriteLine($"{Color.FORE_RED}{enemy.Name}{Color.RESET} attacked {Player.Name}...    -{Color.FORE_LIGHT_RED}{Player.Damage(enemy.AttackDamage)}{Color.RESET}HP");
                }
            }
        }
    }

    private static void InputHandling(string[] input)
    {
        Debug.WriteLine("Input received: " + string.Join(' ', input));
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
                if(input.Length >= 2 && int.TryParse(input[1], out _)) Help.ListHelp(int.Parse(input[1]));
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
                if (!Player.CurrentRoom.Searched)
                {
                    Console.WriteLine($"{Player.Name} is not aware of their surroundings!");
                    break;
                }
                if(input.Length > 1 && input[1].ToLower() == "all")
                {
                    if(Player?.CurrentRoom.Inventory.Count == 0)
                    {
                        Console.WriteLine($"{Player!.Name} found nothing to pick up.");
                        return;
                    }
                    
                    for (int i = Player?.CurrentRoom.Inventory.Count - 1 ?? 0; i >= 0; i--)
                    {
                        Thread.Sleep(800);
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
                            Thread.Sleep(800);
                            
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
            case "goto":
            case "walk":
                if(input.Length < 2) Console.WriteLine($"Please specify the {Color.FORE_WHITE}direction{Color.FORE_WHITE} in which you want to move.");
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
                        Console.WriteLine("The path is blocked, and " + Player?.Name + " can't seem to find a different way around.");
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
                if(input.Length < 2)
                {
                    Console.WriteLine($"Please specify the {Color.FORE_WHITE}person{Color.FORE_WHITE} you want to talk to.");
                    return;
                }
                foreach(NPC? npc in Player!.CurrentRoom.NPCs!)
                {
                    if (npc.Name.Clean().ToLower() != string.Join(' ', input[1..]).ToLower())
                    {
                        continue;
                    }

                    if (npc is FriendlyNPC)
                    {
                        FriendlyNPC trader = npc as FriendlyNPC;
                        trader?.TradeDialogue();
                    }
                    else if (npc is HostileNPC)
                    {
                        HostileNPC enemy = npc as HostileNPC;
                        Console.WriteLine(enemy!.Dialogue);
                    }

                    return;
                }
                Console.WriteLine($"{Player.Name} could not find anyone named \"{Color.FORE_GREEN}{string.Join(' ', input[1..])}{Color.RESET}\" to talk to.");
                break;
            case "trade":
            case "buy":
            case "deal":
                foreach(NPC? npc in Player!.CurrentRoom.NPCs!)
                {
                    Debug.WriteLine(input[^1]);
                    bool isLastNumeric = int.TryParse(input[^1], out _);
                    if (!isLastNumeric)
                    {
                        Console.WriteLine($"Please specify the {Color.FORE_WHITE}item number{Color.RESET} to trade for.");
                        return;
                    }
                    if(npc is not FriendlyNPC) continue;
                    Debug.WriteLine(string.Join(' ', input[1..^1]));
                    if (npc.Name.Clean().ToLower() == string.Join(' ', input[1..^1]).ToLower())
                    {
                        FriendlyNPC? trader = npc as FriendlyNPC;
                        trader?.Trade(int.Parse(input[^1]));
                        return;
                    }
                }
                Console.WriteLine($"{Player.Name} could not find anyone named \"{Color.FORE_GREEN}{string.Join(' ', input[1..^1])}{Color.RESET}\" to trade with.");
                break;
            case "use":
                Console.WriteLine("\"Use\" via the inventory :3");
                break;
            case "attack":
            case "fight":
                if (input.Length < 2)
                {
                    Console.WriteLine($"Please specify the {Color.FORE_WHITE}person{Color.FORE_WHITE} you want to {Color.FORE_RED}attack{Color.RESET}.");
                    return;
                }
                foreach (NPC? npc in Player!.CurrentRoom.NPCs!)
                {
                    if (npc.Name.Clean().ToLower() != string.Join(' ', input[1..]).ToLower() || npc is not HostileNPC)
                    {
                        continue;
                    }

                    HostileNPC enemy = npc as HostileNPC;
                    enemy.Damage(Player.EquippedWeapon.Damage);

                    return;
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