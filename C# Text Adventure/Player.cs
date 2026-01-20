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

    public Player(string name, double maxHP)
    {
        _name = name;
        _maxHP = maxHP;
        _hp = maxHP;
    }
}