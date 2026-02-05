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
            Console.WriteLine($"\"I have something to trade. Look over here.\"\n");
            for(int i = 0; i < Inventory.Count; i++)
            {
                Item item = Inventory[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.ValueText}");
            }
            Console.WriteLine($"\n\"Just tell me {Color.FORE_WHITE}which number{Color.RESET} you want!\"");
            Console.WriteLine($"You have {Program.Player.MoneyText}.");
        }
        public void Trade(int itemIndex)
        {
            if(itemIndex < 1 || itemIndex > Inventory.Count)
            {
                Console.WriteLine($"\"I don't have that item.\"");
                return;
            }
            Item item = Inventory[itemIndex - 1];
            if(Program.Player.Money < item.Value)
            {
                Console.WriteLine($"\"You don't have enough {Color.FORE_LIGHT_GREEN}money{Color.RESET} to purchase that {Color.FORE_CYAN}item{Color.RESET}.\"");
                return;
            }
            try
            {
                Program.Player.Inventory.Add(item);
                Inventory.RemoveAt(itemIndex - 1);
                Program.Player.Money -= item.Value;
                Money += item.Value;
                Console.WriteLine($"{Program.Player.Name} bought {item.Name} for {item.ValueText} from {Name}.");
            }
            catch(ItemTooHeavyException)
            {
                Console.WriteLine($"{Program.Player.Name} cannot think of a way to stuff {item.Name} into his inventory.\nIt is {Color.FORE_WHITE}too heavy{Color.RESET}.");
            }
        }
    }
    public class HostileNPC : NPC
    {
        public int Damage { get; private init; }
        public int Health { get; set; }
        public string? UnlockRoom {get; init;}
        public HostileNPC(string name, string description ,int money, int damage, int health, string dialogue = "They simply stare at you.", string? unlockRoom = null) : base(name, description, money, dialogue)
        {
            Damage = damage;
            Health = health;
            UnlockRoom = unlockRoom;
        }
    }
}
