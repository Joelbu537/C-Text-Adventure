namespace C__Text_Adventure;
using C__Text_Adventure.Items;
public static class InventoryDisplay
{
    private static Player p = Program.Player;
    private static string InfoText = string.Empty;
    private static int SelectedItem = 0;
    private static int SelectedInfo = 0;
    private static bool InfoMode { get; set; } = false;
    private static List<string> ConsoleBuffer = new();
    public static void InventoryLoop()
    {
        DrawInventory();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);


                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        if (InfoMode)
                        {
                            InfoMode = false;
                            SelectedInfo = 0;
                            break;
                        }
                        Console.Clear();
                        return;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (InfoMode)
                        {
                            SelectedInfo++;
                            break;
                        }
                        SelectedItem = Math.Clamp(SelectedItem + 1, 0, p.Inventory.Count - 1);
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (InfoMode)
                        {
                            SelectedInfo--;
                            break;
                        }
                        SelectedItem = Math.Clamp(SelectedItem - 1, 0, p.Inventory.Count - 1);
                        break;
                    case ConsoleKey.Enter:
                        if (InfoMode)
                        {
                            break;
                        }
                        InfoMode = true;
                        break;
                }

                if(p.Inventory.Count == 0) return;
            }
            DrawInventory();
        }
    }

    private static void DrawInventory()
    {
        while(Console.WindowHeight < 14 || Console.WindowWidth < 40)
        {
            Console.Clear();
            Boxing.WindowTooSmall();
            Thread.Sleep(500);
        }

        int itemDisplayCount = Console.WindowHeight - 4; // Ammount of items that can be displayed without scrolling

        ConsoleBuffer.Add(Boxing.WindowCeiling(Console.WindowWidth - 2));  //
        ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 2)); // Top Border

        for(int i = 0; i < itemDisplayCount; i++)
        {
            if(!(i < p.Inventory.Count))    // If there are no more items to display, fill with empty lines
            {
                ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 2));
                continue;
            }
            if(i == SelectedItem)        // Highlight the selected item
            {
                ConsoleBuffer.Add(C__Text_Adventure.Color.BACK_WHITE);
            }
            ConsoleBuffer.Add(Boxing.WindowWall($"{p.Inventory[i].Name}", Console.WindowWidth - 2)); // Display the item
        }

        ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 2)); //
        ConsoleBuffer.Add(Boxing.WindowFloor(Console.WindowWidth - 2));    // Bottom Border


        // Draw the final buffer
        Console.Clear();
        foreach(string line in ConsoleBuffer)
        {
            Console.WriteLine(line);
        }
    }

    private static int GetMiddlePadding(int length)
    {
        return (Console.WindowWidth - length) / 2;
    }

    private static void Use()
    {
        Item targetItem = p.Inventory[SelectedItem];


        if(targetItem is HealthingItem healthItem)
        {
            p.HealthUp(healthItem.HealthUpAmmount);
            p.Heal(healthItem.HealAmount);
            InfoText = $"";
            p.Inventory.RemoveAt(SelectedItem);
        }
        else if (targetItem is HealingItem healItem)
        {
            p.Heal(healItem.HealAmount);
            InfoText = $"{p.Name} used {p.CurrentRoom.Inventory[SelectedItem]}!".ToString(); // Because it is referencing remove objects
            p.Inventory.RemoveAt(SelectedItem);
        }
        else if (targetItem is InfoItem infoItem)
        {
            InfoText = infoItem.Message;
        }
        else if (targetItem is WeaponItem weaponItem)
        {
            p.Inventory.Add(p.EquippedWeapon);
            p.EquippedWeapon = weaponItem;
            p.Inventory.RemoveAt(SelectedItem);

            InfoText = $"{p.Name} is now wielding {weaponItem.Name}!";
        }
        else if (targetItem is ArmorItem armorItem)
        {
            p.Inventory.Add(p.EquippedWeapon);
            p.EquippedArmor = armorItem;
            p.Inventory.RemoveAt(SelectedItem);

            InfoText = $"{p.Name} is now wearing {armorItem.Name}!";
        }
    }

    private static void Drop()
    {
        Item targetItem = p.Inventory[SelectedItem];
        p.CurrentRoom.Inventory.Add(targetItem);
        p.Inventory.RemoveAt(SelectedItem);
        InfoText = $"{p.Name} dropped {p.CurrentRoom.Inventory[SelectedItem]}!";
    }

    private static void Describe()
    {
        Item targetItem = p.Inventory[SelectedItem];
        InfoText = targetItem.Description;
    }
}