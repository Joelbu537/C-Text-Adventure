using System;
using System.Collections.Generic;
using System.Text;

public class Player
{
    private string _name = String.Empty;
    public string Name
    {
        get
        {
            return Color.FORE_LIGHT_CYAN + _name + Color.RESET;
        }
    }

    private double _hp = 0;
    public double HP
    {
        get
        {
            return _hp;
        }
    }

    private double _maxHP = 0;
    public double MaxHP
    {
        get
        {
            return _maxHP;
        }
    }
    public Room CurrentRoom { get; set; }
    public InventoryList Inventory { get; set; }
    public bool InCombat = false;
    public Player(string name, double maxHP, double maxWeight, Room startRoom)
    {
        _name = name;
        _maxHP = maxHP;
        _hp = maxHP;
        Inventory = new(maxWeight);
        CurrentRoom = startRoom;
    }
    public void Status()
    {
        Console.Write(Name + " is ");
        double percentage = HP / MaxHP;
        if(HP < 10)
        {
            Console.WriteLine(Color.BACK_RED + "nearly dead");
        }
        else if(percentage < 0.15)
        {
            Console.WriteLine(Color.FORE_LIGHT_RED + "badly injured");
        }
        else if(percentage < 0.3)
        {
            Console.WriteLine(Color.FORE_YELLOW + "injured");
        }
        else if(percentage < 0.65)
        {
            Console.WriteLine(Color.FORE_LIGHT_YELLOW + "lightly injured");
        }
        else
        {
            Console.WriteLine(Color.FORE_LIGHT_GREEN + "feeling fine");
        }
        Console.Write(Color.RESET);

        Console.WriteLine($"Carrying {Color.FORE_WHITE}{Inventory.Count}{Color.FORE_CYAN} items " + Color.RESET +
            $"with a total weight of {Color.BACK_WHITE}{Color.FORE_BLACK}{Math.Round(Inventory.InventoryWeight, 2)}/{Math.Round(Inventory.MaxInventoryWeight, 2)}{Color.RESET} kg");
    }
}