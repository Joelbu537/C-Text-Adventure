using TextAdventure.Items;

namespace TextAdventure
{
    public class NPC
    {
        public string Name { get; private init; }
        public int Money { get; set; }
        public string Description { get; private init; }
        public string Dialogue { get; set; }
        public string MoneyText
        {
            get
            {
                return Color.FORE_WHITE + '$' + Color.FORE_GREEN + Money.ToString() + Color.RESET;
            }
        }
        public NPC(string name, string description, int money, string dialogue = "They do not respond.")
        {
            Name = name;
            Description = description;
            Money = money;
            Dialogue = dialogue;
        }
    }
    public class FriendlyNPC : NPC
    {
        private InventoryList? _inventory;
        public InventoryList Inventory
        {
            get
            {
                return _inventory!;
            }
            set
            {
                _inventory = value;
            }
        }
        public FriendlyNPC(string name, string description, int money, string dialogue = "They do not respond.") : base(name, description, money, dialogue)
        {
            _inventory = new InventoryList();
        }
        public void TradeDialogue()
        {
            if(Inventory.Count == 0)
            {
                Console.WriteLine($"\"I have nothing to trade.\"");
                return;
            }
            Console.WriteLine($"\"I have something to trade. Look over here.\"");
            for(int i = 0; i < Inventory.Count; i++)
            {
                Item item = Inventory[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.ValueText}");
            }
        }
    }
    public class HostileNPC : NPC
    {
        public int Damage { get; private init; }
        public int Health { get; set; }
        public HostileNPC(string name, string description ,int money, int damage, int health, string dialogue = "They stare at you.") : base(name, description, money, dialogue)
        {
            Damage = damage;
            Health = health;
        }
    }
}
