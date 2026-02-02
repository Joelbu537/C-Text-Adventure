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
                    /*case ConsoleKey.DownArrow:
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
                        */
                }

                if(p.Inventory.Count == 0) return;

                DrawInventory();
            }
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

        Console.Clear();

        Console.WriteLine(Boxing.WindowCeiling(Console.WindowWidth - 2));
        Console.WriteLine(Boxing.WindowWall("", Console.WindowWidth - 2));
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