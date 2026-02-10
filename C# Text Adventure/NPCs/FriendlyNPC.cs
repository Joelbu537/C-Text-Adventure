namespace TextAdventure.NPCs;
using TextAdventure.Items;
public class FriendlyNPC : NPC
{
    public override string Name => Color.FORE_GREEN + base.Name + Color.RESET;
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
        int maxLength = 0;
        for(int i = 0; i < Inventory.Count; i++)
        {
            maxLength = Math.Max(maxLength, Inventory[i].Name.Clean().Length);
        } 
        for(int i = 0; i < Inventory.Count; i++)
        {
            Item item = Inventory[i];
            Console.WriteLine($"{i + 1}. {item.Name}{new string(' ', maxLength - item.Name.Clean().Length)} - {item.ValueText}");
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
            Console.WriteLine($"\"Sorry, but you don't have enough {Color.FORE_LIGHT_GREEN}money{Color.RESET} to purchase that {Color.FORE_CYAN}item{Color.RESET}...\"");
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