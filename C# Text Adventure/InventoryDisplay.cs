namespace C__Text_Adventure;
using C__Text_Adventure.Items;
using static Color;
public static class InventoryDisplay
{
    private static Player p = Program.Player!;
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
                DrawInventory();
            }
        }
    }
    private static void DrawInventory()
    {
        while (Console.WindowHeight < 19 || Console.WindowWidth < 35)
        {
            Console.Clear();
            Boxing.WindowTooSmall();
            Thread.Sleep(500);
        }

        int itemDisplayCount = Console.WindowHeight - 5; // Ammount of items that can be displayed without scrolling, -2 for roof, -2 for floor, -1 for empty line (so the roof does not get pusehd out of the visible console).

        ConsoleBuffer.Add(Boxing.WindowCeiling(Console.WindowWidth - 3));  //
        ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 4)); // Top Border

        for (int i = 0; i < itemDisplayCount; i++)
        {
            if (!(i < p.Inventory.Count))    // If there are no more items to display, fill with empty lines
            {
                ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 4));
                continue;
            }
            ConsoleBuffer.Add(Boxing.WindowWall($"{(i == SelectedItem ? BACK_WHITE : "")}{p.Inventory[i].Name}{RESET}", Console.WindowWidth - 4)); // Display the item, if it's selected, highlight it.
        }

        ConsoleBuffer.Add(Boxing.WindowWall("", Console.WindowWidth - 4)); // Bottom Border
        ConsoleBuffer.Add(Boxing.WindowFloor(Console.WindowWidth - 3));    //


        if(InfoMode)    // Draw Info Window  // Size should scale depending on Description size.
        {
            int descriptionLines = Console.WindowHeight - 10 - 2; // Lines available for description, - 10 for interactions and roof, - 2 for floor.
            int infoTotalWidth = (Console.WindowWidth - 4) / 2; // Total width of info box, should be half of main window minus the outer border.
            int infoInnerWidth = infoTotalWidth - 4; // Inner width of info box, without walls and space.
            int descriptionChars = descriptionLines * infoInnerWidth; // Total characters that can fit in description area.

            // Info Box Drawing
            int insertionIndex = Console.WindowWidth - infoTotalWidth - 2;

            ConsoleBuffer[2] = ConsoleBuffer[2].OverwriteAt(Boxing.WindowCeiling(infoTotalWidth - 2), insertionIndex);                                      // Info Roof
            ConsoleBuffer[3] = ConsoleBuffer[3].OverwriteAt(Boxing.WindowWall("", infoInnerWidth), insertionIndex);                                         // Empty line
            ConsoleBuffer[4] = ConsoleBuffer[4].OverwriteAt(Boxing.WindowWall(Boxing.Center($"{(SelectedInfo == 0 ? BACK_GREEN : BACK_BLACK)}USE Item{RESET}",
                infoInnerWidth), infoInnerWidth), insertionIndex);                                                                                          // Info USE Button
            ConsoleBuffer[5] = ConsoleBuffer[5].OverwriteAt(Boxing.WindowWall("", infoInnerWidth), insertionIndex);                                         // Empty Line
            ConsoleBuffer[6] = ConsoleBuffer[6].OverwriteAt(Boxing.WindowWall(Boxing.Center($"{(SelectedInfo == 1 ? BACK_LIGHT_RED : BACK_BLACK)}DROP Item{RESET}",
                infoInnerWidth), infoInnerWidth), insertionIndex);                                                                                          // Info DROP line  
            ConsoleBuffer[7] = ConsoleBuffer[7].OverwriteAt(Boxing.WindowWall("", infoInnerWidth), insertionIndex);                                         // Empty Line
            ConsoleBuffer[8] = ConsoleBuffer[8].OverwriteAt(Boxing.WindowWall(Boxing.Center($"{(SelectedInfo == 2 ? BACK_LIGHT_YELLOW : BACK_BLACK)}SELL Item{RESET}",
                infoInnerWidth), infoInnerWidth), insertionIndex);                                                                                          // Info SELL line  
            ConsoleBuffer[9] = ConsoleBuffer[9].OverwriteAt(Boxing.WindowWall("", infoInnerWidth), insertionIndex);                                         // Empty Line
        }


        // Draw the final buffer
        Console.Clear();
        foreach (string line in ConsoleBuffer)
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