namespace C__Text_Adventure;
using C__Text_Adventure.Items;
public static class InventoryDisplay
{
    private static Player p = Program.Player;
    const string InventoryText = "[INVENTORY]";
    private static string InfoText = String.Empty;
    private static int SelectedItem { get; set; } = 0;
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
                        Console.Clear();
                        return;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        SelectedItem = Math.Max(SelectedItem + 1, p.Inventory.Count - 1);
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        SelectedItem = Math.Max(0, SelectedItem - 1);
                        break;
                    case ConsoleKey.Enter:
                        Use();
                        break;
                    case ConsoleKey.R:
                        Drop();
                        break;
                    case ConsoleKey.I:
                        Describe();
                        break;
                }

                if(p.Inventory.Count == 0) return;

                DrawInventory();
            }
        }
    }

    private static void DrawInventory()
    {
        Console.Clear();
        /*
         * Inventory LABEL
         * Empty
         * Scroll Up
         * Inventory 1
         * Inventory 2
         * Inventory 3
         * Inventory 4
         * INVENTORY 5
         * Inventory 6
         * Inventory 7
         * Inventory 8
         * Inventory 9
         * Scroll Down
         * Empty
         * InfoText
         * Keys
         */


        // Draw Inventory Label
        int inventoryTextSideWidth = GetMiddlePadding(InventoryText.Length);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(String.Empty.PadRight(inventoryTextSideWidth) + Color.BACK_WHITE + InventoryText + Color.RESET + String.Empty.PadRight(inventoryTextSideWidth) + '\n');


        // Draw scroll up indicator
        if (SelectedItem > 5) 
        {
            int scrollSideWidth = GetMiddlePadding(1);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(String.Empty.PadRight(scrollSideWidth) + '⮝' + String.Empty.PadRight(scrollSideWidth) + '\n');
            Console.ResetColor();
        }
        Console.ResetColor();

        // Draw items
        for (int i = 0; i < 9; i++)
        {
            Console.ResetColor();

            int targetItem = SelectedItem - 5 + i;
            if (targetItem < 0 || targetItem >= p.Inventory.Count)
            {
                Console.WriteLine();
                continue;
            }

            string itemSentence = Color.FORE_CYAN;
            if (i == 5)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }

            int middlePadding = GetMiddlePadding(p.Inventory[targetItem].RawName.Length);
            itemSentence = itemSentence.Insert(0, new string(' ', middlePadding));
            itemSentence += p.Inventory[targetItem].RawName;
            itemSentence = itemSentence.Insert(itemSentence.Length - 1, new string(' ', middlePadding));
            Console.WriteLine(itemSentence);
        }

        // Draw scroll down indicator
        if (p.Inventory.Count > SelectedItem + 5) // Draw scroll up indicator
        {
            int scrollSideWidth = GetMiddlePadding(1);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(String.Empty.PadRight(scrollSideWidth) + '⮟' + String.Empty.PadRight(scrollSideWidth) + '\n');
            Console.ResetColor();
        }
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine(InfoText);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine($"{Color.BACK_WHITE}[ENTER]{Color.BACK_LIGHT_GREY} Use Item {Color.BACK_BLACK}    " +
                          $"{Color.BACK_WHITE}[I]{Color.BACK_LIGHT_GREY} Inspect Item {Color.BACK_BLACK}    " +
                          $"{Color.BACK_WHITE}[R]{Color.BACK_LIGHT_GREY} Drop Item {Color.BACK_BLACK}");
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