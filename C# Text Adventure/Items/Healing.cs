namespace TextAdventure.Items;
public class HealingItem : Item
{
    public int HealAmount { get; private init; }
    internal HealingItem(string name, string description, double weight, int value, int healAmount)
        : base(name, description, weight, value)
    {
        HealAmount = healAmount;
    }
}
public static class Healing
{
    public static Item Corn = new HealingItem(
        "Corncob",
        "A large, ripe, yellow, cob of corn.\nBest served with butter, but definetly edible in its current form.",
        0.5,
        10,
        10
    );
    public static Item HealingPotion = new HealingItem(
        "Healing Potion",
        "A clear glass bottle selaed with a cork.\nIt contains a red liquid that smells like cheryy.",
        0.25,
        50,
        75
    );

    public static Item FabricLump = new HealingItem(
        "Lump of fabric",
        "A bunch of small pieces of fabric in bad condition.\nCan still be used to stop a bleeding or cover up a wound.",
        0.5,
        5,
        25
    );
    public static Item HealingHerbs = new HealingItem(
        "Medical Herbs",
        "\"For both body and mind\" it says on the bag.",
        0.2,
        20,
        30
    );
    public static Item Beer = new HealingItem(
        "Beer",
        "A glass filled with beer.",
        0.33,
        4,
        4
    );
}