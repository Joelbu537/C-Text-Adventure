using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace C__Text_Adventure
{
    public class NPC
    {
        public string Name { get; private init; }
        public string RawName {get; private init; }
        public int Money { get; set; }
        public string Description { get; private init; }

        public string MoneyText
        {
            get
            {
                return Color.FORE_WHITE + '$' + Color.FORE_GREEN + Money.ToString() + Color.RESET;
            }
        }
        public NPC(string name, string description, int money)
        {
            Name = name;
            Description = description;
            Money = money;
        }
    }
    public class FriendlyNPC : NPC
    {
        public InventoryList Inventory { get; set; }
        public FriendlyNPC(string name, string description, int money) : base(name, description, money)
        {
        }
        public void Trade()
        {
            
        }
    }
    public class HostileNPC : NPC
    {
        public int Damage { get; private init; }
        public int Health { get; set; }
        public HostileNPC(string name, string description ,int money, int damage, int health) : base(name, description, money)
        {
            Damage = damage;
            Health = health;
        }
    }
}
