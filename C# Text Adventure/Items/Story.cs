namespace C__Text_Adventure.Items;
public class InfoItem : Item
{
    public string Message { get; private init; }
    internal InfoItem(string name, string description, double weight, double value, string message)
        : base(name, description, weight, value)
    {
        Message = message;
    }
}
public static class Story
{
    public static Item TavernNote = new InfoItem(
        "Note", "A piece of paper with something written on it",
        0.01,
        1,
        "We need your help!\nMeet me in the shed on the " + Color.FORE_WHITE + "east" + Color.RESET + " side of the tavern!"
    );
}