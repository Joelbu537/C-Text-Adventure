public class Room
{
    public string Name { get; private init; }
    public string Description { get; private init; }
    public InventoryList Inventory { get; set; }
    public Room[] ConnectedRooms { get; private init; }
    public bool IsUnlocked { get; private set; }
    public string? FirstEnterMessage { get; private init; }
    private bool hasBeenEntered = false;

    public Room(string name, string description, bool isUnlocked, InventoryList inventory, Room[] connectedRooms, string firstEnterMessage = null)
    {
        Name = name;
        Description = description;
        IsUnlocked = isUnlocked;
        Inventory = inventory;
        ConnectedRooms = new Room[4];
        Array.Copy(connectedRooms, ConnectedRooms, connectedRooms.Length);
        FirstEnterMessage = firstEnterMessage;
    }
    public void Describe()
    {
        Console.WriteLine(Color.FORE_LIGHT_GREEN + Program.Player.Name + Color.RESET + " is here: " + Color.FORE_WHITE + Name + Color.RESET);
        Console.WriteLine(Description);
        if (!hasBeenEntered && FirstEnterMessage != null)
        {
            Console.WriteLine(FirstEnterMessage);
        }
        hasBeenEntered = true;
    }
}
public enum RoomDirection
{
    North,
    South,
    East,
    West
}