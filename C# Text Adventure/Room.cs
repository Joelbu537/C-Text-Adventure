namespace TextAdventure;
using TextAdventure.Items;
using TextAdventure.NPCs;
public class Room
{
    private string _name;
    public string Name
    {
        get
        {
            return Color.FORE_WHITE + _name + Color.RESET;
        }
    }

    private string Description { get; }
    public bool Searched { get; set; } = false;
    public InventoryList Inventory { get; }
    public List<NPC> NPCs = new();
    private Room?[]? connectedRooms;
    public Room?[] ConnectedRooms 
    { 
        get
        {
            return connectedRooms!;
        }
        set
        {
            connectedRooms = value;
        } 
    }
    public bool IsUnlocked { get; set; }
    private bool hasBeenEntered = false;
    public event Program.Action? OnFirstEntry;

    public Room(string name, string description, bool isUnlocked, Program.Action? onFirstEntry = null)
    {
        _name = name;
        Description = description;
        IsUnlocked = isUnlocked;
        Inventory = new();
        if (onFirstEntry != null)
        {
            OnFirstEntry += onFirstEntry;
        }
    }
    public void Describe()
    {
        Console.WriteLine(Program.Player!.Name + " is here: " + Name);
        Console.WriteLine(Description);

        if (!hasBeenEntered && OnFirstEntry != null)
        {
            OnFirstEntry();
        }
        hasBeenEntered = true;

        if(NPCs != null && NPCs.Count != 0)
        {
            Console.WriteLine($"\n{Color.FORE_WHITE}Persons{Color.RESET} that seem to be not completely irrelevant:\n");
            foreach(NPC npc in NPCs)
            {
                Console.WriteLine($"{npc.Name} - {npc.Description}");
            }
        }

        
        if (!hasBeenEntered)
        {
            OnFirstEntry();
        }
        hasBeenEntered = true;
    }
    public void Search()
    {
        Console.WriteLine(Program.Player?.Name + " is searching " + Name + "...");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("They did not find anything of relevance.");
            return;
        }

        Console.WriteLine("They found the following " + Color.FORE_CYAN + "items" + Color.RESET + ":");
        for (int i = 0; i < Inventory.Count; i++)
        {
            if(!Searched) Thread.Sleep(1500);
            Console.WriteLine(Inventory[i].Name);
        }

        Searched = true;
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }
}
public enum RoomDirection
{
    north,
    south,
    east,
    west
}