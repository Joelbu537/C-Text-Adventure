using System.Diagnostics;

public static class ItemDefinitions
{
    public static Item Corn = new HealingItem(
        "Corncob",
        "A large, ripe, yellow, cob of corn. Best served with butter, but definetly edible in its current form.",
        0.5,
        5,
        15
        );
    public static Item HealingPotion = new HealingItem(
        "Healing Potion",
        "A clear glass bottle selaed with a cork. It contains a red liquid that smells like cheryy.",
        0.25,
        50,
        75
    );

    public static Item TavernNote = new InfoItem("Note", "A piece of paper with something written on it", 0.01, 1,
        "We need your help!\nMeet me in the shed on the " + Color.FORE_WHITE + "east" + Color.RESET + " side of the tavern!");
}