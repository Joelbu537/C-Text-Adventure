namespace TextAdventure;
using TextAdventure.Items;
using TextAdventure.NPCs;
public static class RoomDefinitions
{
    private static Room? startingField;
    public static Room StartingField { 
        get => startingField!; 
        private set => startingField = value; 
    }

    private static Room? tavern;
    public static Room Tavern { 
        get => tavern!; 
        private set => tavern = value; 
    }

    private static Room? tavernShed;
    public static Room TavernShed { 
        get => tavernShed!; 
        private set => tavernShed = value; 
    }

    private static Room? basementStairs;
    public static Room BasementStairs
    {
        get => basementStairs!;
        private set => basementStairs = value;
    }

    private static Room? basement;
    public static Room Basement
    {
        get => basement!;
        private set => basement = value;
    }
    public static readonly List<Room> Rooms = new List<Room>
    {
        StartingField,
        Tavern,
        TavernShed,
        BasementStairs,
        Basement
    };

    public static void InitRooms()
    {
        StartingField = new Room(
            name: "Field",
            description: "A empty field that stretches as far as " + Program.Player!.Name + " can see.\nThe only thing that is not grass seems to be a " + Color.FORE_WHITE + "tavern" + Color.RESET + " to your " + Color.FORE_WHITE + "north" + Color.RESET,
            isUnlocked: true
        );
        StartingField.AddItem(Healing.Corn);
        StartingField.AddItem(Healing.FabricLump);

        Tavern = new Room(
            name: "Tavern",
            description: "A crowded tavern with all kinds of people inside of it. It smells like beer and yeast.",
            isUnlocked: true,
            firstEnterMessage: "The host secretly slides a " + Color.FORE_CYAN + "note" + Color.FORE_WHITE + " towards " + Program.Player.Name + Color.FORE_WHITE + " and quickly leaves."
        );
        Tavern.AddItem(Story.TavernNote);
        Tavern.AddItem(Healing.Beer);

        TavernShed = new Room(
            name: "Shed",
            description: "A small shed with some pigs inside, illuminated in a warm yellow light.\n" +
                Program.Player.Name + " can still hear the noise coming from the tavern to their " + Color.FORE_WHITE + "west" + Color.RESET + 
                    "through the thinn wooden planks seperating the two buildings.",
            isUnlocked: true,
            firstEnterMessage: "A man dressed like a blacksmith who is standing at a table at the end of the shed is waving at " + Program.Player.Name + '.'
        );
        TavernShed.AddItem(Weapon.WoodenClub);
        TavernShed.AddItem(Healing.HealingHerbs);
        TavernShed.AddItem(Armor.LeatherArmor);
        TavernShed.AddItem(Healthing.LifePotion);

        BasementStairs = new Room(
            name: "Staircase leading to Basement",
            description: $"A staircase made from cobblestone, leading directly into the ground. It leads to a {Color.FORE_WHITE}room{Color.RESET} in the {Color.FORE_WHITE}west{Color.RESET}. It smells damp. ",
            isUnlocked: true,
            firstEnterMessage: "A skeleton walks arround a corner and looks at you. Then, it starts screaminga nd smashing its sword against its shield."
        );

        Basement = new Room(
            name: "Basement below the Tavern",
            description: $"A dark room with walls made out of stone. {Program.Player.Name} can glimpse the outline of a few {Color.FORE_CYAN}items{Color.RESET} lying arround.\n" +
                $"A set of {Color.FORE_WHITE}stairs{Color.RESET} to the {Color.FORE_WHITE}eastern{Color.RESET} side leads up.",
            isUnlocked: false
        );


        StartingField.ConnectedRooms = [Tavern, null, null, null];
        Tavern.ConnectedRooms = [null, StartingField, TavernShed, null];
        TavernShed.ConnectedRooms = [null, BasementStairs, null, Tavern];
        BasementStairs.ConnectedRooms = [TavernShed, null, null, Basement];
        Basement.ConnectedRooms = [null, null, BasementStairs, null];

        TavernShed.NPCs = [FriendlyNPCDefinitions.Blacksmith];
        BasementStairs.NPCs = [HostileNPCDefinitions.Skeleton];
    }
}