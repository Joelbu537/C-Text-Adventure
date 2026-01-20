public class Item
{
    public string Name { get; private init; }
    public string Description { get; private init; }
    public double Weight { get; private init; }
    public Item(string name, string description, double weight)
    {
        Name = name;
        Description = description;
        Weight = weight;
    }
}