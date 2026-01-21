public class Item
{
    public string RawName { get; private init; }
    public string Name
    {
        get
        {
            return Color.FORE_CYAN + RawName + Color.RESET;
        }
    }

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

    public Item(string name, string description, double weight, double value)
    {
        RawName = name;
        Description = description;
        Weight = weight;
        Value = value;
    }
}
public class HealingItem : Item
{
    public int HealAmount { get; private init; }
    public HealingItem(string name, string description, double weight, double value, int healAmount)
        : base(name, description, weight, value)
    {
        HealAmount = healAmount;
    }
}
public class WeaponItem : Item
{
    public int Damage { get; private init; }
    public WeaponItem(string name, string description, double weight, double value, int damage)
        : base(name, description, weight, value)
    {
        Damage = damage;
    }
}
public class InfoItem : Item
{
    public string Message { get; private init; }
    public InfoItem(string name, string description, double weight, double value, string message)
        : base(name, description, weight, value)
    {
        Message = message;
    }
}