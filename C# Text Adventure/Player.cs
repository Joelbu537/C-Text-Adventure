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
            return Color.FORE_LIGHT_GREEN + _name + Color.RESET;
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
    public Player(string name, double maxHP, Room startRoom)
    {
        _name = name;
        _maxHP = maxHP;
        _hp = maxHP;
        Inventory = new();
        CurrentRoom = startRoom;
        CurrentRoom.Describe();
    }
    public void Status()
    {
        Console.Write(Name + " is ");
        double percentage = HP / MaxHP;
        if(HP < 10)
        {
            Console.WriteLine(Color.BACK_RED + "nearly dead");
        }
        else if(percentage < 15)
        {
            Console.WriteLine(Color.FORE_LIGHT_RED + "badly injured");
        }
        else if(percentage < 30)
        {
            Console.WriteLine(Color.FORE_YELLOW + "injured");
        }
        else if(percentage < 70)
        {
            Console.WriteLine(Color.FORE_LIGHT_YELLOW + "lightly injured");
        }
        else
        {
            Console.WriteLine(Color.FORE_GREEN + "fine");
        }
        Console.Write(Color.RESET);

        Console.WriteLine($"Carrying {Color.FORE_WHITE}{Inventory.Count}{Color.RESET} itesm " +
            $"with a total weight of {Color.BACK_WHITE}{Color.FORE_BLACK}{Inventory.InventoryWeight}/{Inventory.MaxInventoryWeight}{Color.RESET} kg");
    }
}