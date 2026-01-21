public static class RoomDefinitions
{
    public static Room StartingField = new Room(
        "Field",
        "A empty field that stretches as far as you can see.\nThe only thing that is not grass seems to be a " + Color.FORE_WHITE + "tavern" + Color.RESET + " to your " + Color.FORE_WHITE + "north" + Color.RESET,
        true,
        new Room[] { Tavern }
    );

    public static Room Tavern = new Room(
        "Tavern",
        "A crowded tavern with all kinds of people inside of it. It smells like beer and yeast.",
        true,
        new Room[] { null, null, StartingField },
        "The host secretly slides a note towards you."
    );

    static RoomDefinitions()
    {
        StartingField.AddItem(ItemDefinitions.Corn);
    }
}