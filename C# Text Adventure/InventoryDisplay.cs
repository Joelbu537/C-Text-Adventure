public static class InventoryDisplay
{
    const string InventoryText = "[INVENTORY]";
    private static int SelectedItem { get; set; } = 0;
    public static void InventoryLoop()
    {
        DrawInventory();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return;
                }

                DrawInventory();
            }
        }
    }

    private static void DrawInventory()
    {
        Console.Clear();

        int inventoryHeight = Console.WindowHeight - 2 - 5; // - Place for Title and free line - whatever space for command selection

        int inventoryTextSideWidth = GetMiddlePadding(InventoryText.Length);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(String.Empty.PadRight(inventoryTextSideWidth) + Color.BACK_WHITE + InventoryText + Color.RESET + String.Empty.PadRight(inventoryTextSideWidth) + '\n');

        if (SelectedItem > inventoryHeight)
        {
            int scrollSideWidth = GetMiddlePadding(1);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(String.Empty.PadRight(scrollSideWidth) + '⌃' + String.Empty.PadRight(scrollSideWidth) + '\n');
            Console.ResetColor();
        }
        Console.ResetColor();
    }

    private static int GetMiddlePadding(int length)
    {
        return (Console.WindowWidth - length) / 2;
    }
}