public class Item
{
    private string _name;
    public string Name
    {
        get
        {
            return Color.FORE_BLUE + _name + Color.RESET;
        }
        set
        {
            _name = value;
        }
    }

    public string Description { get; private init; }
    public double Weight { get; private init; }
    public Item(string name, string description, double weight)
    {
        Name = name;
        Description = description;
        Weight = weight;
    }
}
public class HealingItem : Item
{
    public int HealAmount { get; private init; }
    public HealingItem(string name, string description, double weight, int healAmount)
        : base(name, description, weight)
    {
        HealAmount = healAmount;
    }
}