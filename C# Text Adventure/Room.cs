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
    public InventoryList Inventory { get; }
    public Room?[] ConnectedRooms { get; set; }
    public bool IsUnlocked { get; private set; }
    public string? FirstEnterMessage { get; }
    private bool hasBeenEntered = false;

    public Room(string name, string description, bool isUnlocked, string firstEnterMessage = null)
    {
        _name = name;
        Description = description;
        IsUnlocked = isUnlocked;
        Inventory = new();
        FirstEnterMessage = firstEnterMessage;
    }
    public void Describe()
    {
        Console.WriteLine(Program.Player.Name + " is here: " + Name);
        Console.WriteLine(Description);
        if (!hasBeenEntered && FirstEnterMessage != null)
        {
            Console.WriteLine(Color.FORE_WHITE + FirstEnterMessage + Color.RESET);
        }
        hasBeenEntered = true;
    }
    public void Search()
    {
        Console.WriteLine(Program.Player.Name + " is searching " + Name + "...");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("They did not find anything of relevance.");
            return;
        }

        Console.WriteLine("They found the following " + Color.FORE_CYAN + "items" + Color.RESET + ":");
        for (int i = 0; i < Inventory.Count; i++)
        {
            Console.WriteLine(Inventory[i].Name);
        }
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