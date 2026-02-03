namespace TextAdventure;
using TextAdventure.Items;
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

    public static void InitRooms()
    {
        StartingField = new Room(
        "Field",
        "A empty field that stretches as far as " + Program.Player!.Name + " can see.\nThe only thing that is not grass seems to be a " + Color.FORE_WHITE + "tavern" + Color.RESET + " to your " + Color.FORE_WHITE + "north" + Color.RESET,
        true
        );
        StartingField.AddItem(Healing.Corn);
        StartingField.AddItem(Healing.FabricLump);

        Tavern = new Room(
            "Tavern",
            "A crowded tavern with all kinds of people inside of it. It smells like beer and yeast.",
            true,
            "The host secretly slides a " + Color.FORE_CYAN + "note" + Color.FORE_WHITE + " towards " + Program.Player.Name + Color.FORE_WHITE + " and quickly leaves."
        );
        Tavern.AddItem(Story.TavernNote);
        Tavern.AddItem(Healing.Beer);

        TavernShed = new Room(
            "Shed",
            "A small shed with some pigs inside, illuminated in a warm yellow light.\n" +
            Program.Player.Name + " can still hear the noise coming from the tavern to their " + Color.FORE_WHITE + "west" + Color.RESET + " through the thinn wooden planks seperating the two buildings.",
            true,
            "A man dressed like a blacksmith who is standing at a table at the end of the shed is waving at " + Program.Player.Name + '.'
        );
        TavernShed.AddItem(Weapon.WoodenClub);
        TavernShed.AddItem(Healing.HealingHerbs);
        TavernShed.AddItem(Armor.LeatherArmor);
        TavernShed.AddItem(Healthing.LifePotion);


        Tavern.ConnectedRooms = [null, StartingField, TavernShed, null];
        StartingField.ConnectedRooms = [Tavern, null, null, null];
        TavernShed.ConnectedRooms = [null, null, null, Tavern];

        TavernShed.NPCs = [NPCDefinitions.Blacksmith];
    }
}