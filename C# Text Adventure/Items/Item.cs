namespace TextAdventure.Items;
public abstract class Item
{
    public string RawName { get; private init; }
    public virtual string Name => Color.FORE_CYAN + RawName + Color.RESET;
    public string Description { get; private init; }
    public double Weight { get; private init; }
    public int Value { get; private init; }

    public string ValueText => Color.BACK_LIGHT_GREEN + Color.FORE_WHITE + '$' + Value.ToString("0.00") + Color.RESET;
    public int SellValue {get; private init;}
    public string SellValueText => Color.BACK_LIGHT_GREEN + Color.FORE_WHITE + '$' + SellValue.ToString("0.00") + Color.RESET;

    internal Item(string name, string description, double weight, int value)
    {
        RawName = name;
        Description = description;
        Weight = weight;
        Value = value;
        SellValue = Convert.ToInt32(value / 10 * 9);
    }
}