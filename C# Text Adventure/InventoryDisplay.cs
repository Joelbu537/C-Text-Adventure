using System.Runtime.InteropServices.JavaScript;

public static class InventoryDisplay
{
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
                        SelectedItem = Math.Max(SelectedItem + 1, Program.Player.Inventory.Count - 1);
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

                DrawInventory();
            }
        }
    }

    private static void DrawInventory()
    {
        Console.Clear();
        /*
         * Inventory
         * Empty
         * Scroll Up
         * INVENTORY
         * Scroll Down
         * Empty
         * InfoText
         * Empty
         * Keys
         * Empty
         */
        int inventoryHeight = Console.WindowHeight - 2 - 5; // - Place for Title and free line - whatever space for command selection
        int inventoryHalfBlockShit = (int)Math.Ceiling((double)(inventoryHeight / 2));
        if (inventoryHeight % 2 == 0) inventoryHeight = Math.Max(0, inventoryHeight - 1);

        int inventoryTextSideWidth = GetMiddlePadding(InventoryText.Length);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(String.Empty.PadRight(inventoryTextSideWidth) + Color.BACK_WHITE + InventoryText + Color.RESET + String.Empty.PadRight(inventoryTextSideWidth) + '\n');


        // Draw scroll up indicator
        if (SelectedItem - inventoryTextSideWidth + 1 > 0) 
        {
            int scrollSideWidth = GetMiddlePadding(1);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(String.Empty.PadRight(scrollSideWidth) + '⮝' + String.Empty.PadRight(scrollSideWidth) + '\n');
            Console.ResetColor();
        }
        Console.ResetColor();

        // Draw items
        for (int i = 0; i < inventoryHeight; i++)
        {
            int targetItem = SelectedItem - 2 + i;
            if (targetItem < 0 || targetItem > Program.Player.Inventory.Count - 1)
            {
                Console.WriteLine();
                continue;
            }

            string itemSentence = Color.FORE_CYAN;
            if (i == inventoryHalfBlockShit)
            {
                itemSentence += Color.BACK_WHITE;
            }

            int middlePadding = GetMiddlePadding(Program.Player.Inventory[targetItem].Name.Length);
            itemSentence = itemSentence.Insert(0, new string(' ', middlePadding));
            itemSentence += Program.Player.Inventory[targetItem].Name;
            itemSentence = itemSentence.Insert(itemSentence.Length - 1, new string(' ', middlePadding));
            Console.WriteLine(itemSentence);
        }

        // Draw scroll down indicator
        if (SelectedItem + inventoryHalfBlockShit - 1 < Program.Player.Inventory.Count - 1) // Draw scroll up indicator
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
        if (Program.Player.Inventory[SelectedItem] is HealingItem healItem)
        {
            Program.Player.Heal(healItem.HealAmount);
            InfoText = $"{Program.Player.Name} used {Program.Player.CurrentRoom.Inventory[SelectedItem]}!".ToString(); // Because it is referencing remove objects
            Program.Player.Inventory.RemoveAt(SelectedItem);
            return;
        }

        if (Program.Player.Inventory[SelectedItem] is InfoItem infoItem)
        {
            InfoText = infoItem.Message;
            return;
        }
    }

    private static void Drop()
    {
        Item targetItem = Program.Player.Inventory[SelectedItem];
        Program.Player.CurrentRoom.Inventory.Add(targetItem);
        Program.Player.Inventory.RemoveAt(SelectedItem);
        InfoText = $"{Program.Player.Name} dropped {Program.Player.CurrentRoom.Inventory[SelectedItem]}!";
    }

    private static void Describe()
    {
        Item targetItem = Program.Player.Inventory[SelectedItem];
        InfoText = targetItem.Description;
    }
}