using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace C__Text_Adventure
{
    public class NPC
    {
        public string Name { get; private init; }
        public int Money { get; set; }

        public string MoneyText
        {
            get
            {
                return Color.FORE_WHITE + '$' + Color.FORE_GREEN + Money.ToString() + Color.RESET;
            }
        }
        public NPC(string name, int money)
        {
            Name = name;
            Money = money;
        }
    }
    public class FriendlyNPC : NPC
    {
        public InventoryList Inventory { get; private set; }
        public FriendlyNPC(string name, int money) : base(name, money)
        {
        }
    }
}
