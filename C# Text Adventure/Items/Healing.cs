namespace C__Text_Adventure.Items;
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
}