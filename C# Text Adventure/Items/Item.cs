namespace TextAdventure.Items;
public abstract class Item
{
    public string RawName { get; private init; }
    public virtual string Name => Color.FORE_CYAN + RawName + Color.RESET;
    public string Description { get; private init; }
    public double Weight { get; private init; }
    public double Value { get; private init; }

    public string ValueText
    {
        get
        {
            return Color.BACK_LIGHT_GREEN + Color.FORE_WHITE + '$' + Value.ToString("0.00") + Color.RESET;
        }
    }

    internal Item(string name, string description, double weight, double value)
    {
        RawName = name;
        Description = description;
        Weight = weight;
        Value = value;
    }
}