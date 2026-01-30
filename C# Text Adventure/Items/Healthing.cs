namespace C__Text_Adventure.Items;
public class HealthingItem : HealingItem
{
    public int HealthUpAmmount {get; private init;}
    internal HealthingItem(string name, string description, double weight, double value, int healAmount, int healthUpAmmount)
        : base(name, description, weight, value, healAmount)
    {
        HealthUpAmmount = healthUpAmmount;
    }
}
public static class Healthing
{
    public static Item LifePotion = new HealthingItem(
        "Potion of Life",
        "A small glass tube filled with a clear liquid.",
        0.15,
        75,
        10,
        20
    );
}