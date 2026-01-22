using System.Diagnostics;

public static class ItemDefinitions
{
    public static Item Corn = new HealingItem(
        "Corncob",
        "A large, ripe, yellow, cob of corn. Best served with butter, but definetly edible in its current form.",
        0.5,
        5,
        10
    );
    public static Item HealingPotion = new HealingItem(
        "Healing Potion",
        "A clear glass bottle selaed with a cork. It contains a red liquid that smells like cheryy.",
        0.25,
        50,
        75
    );

    public static Item FabricLump = new HealingItem(
        "Lump of fabric",
        "A bunch of small pieces of fabric in bad condition. Can still be used to stop a bleeding or cover up a wound.",
        0.5,
        5,
        25
    );
    public static Item Glock19 = new WeaponItem(
        "Glock 19",
        "Its a Glock 19. From the future. Infinite ammo included.",
        0.8,
        750,
        100
    );

    public static Item TavernNote = new InfoItem(
        "Note", "A piece of paper with something written on it",
        0.01,
        1,
        "We need your help!\nMeet me in the shed on the " + Color.FORE_WHITE + "east" + Color.RESET + " side of the tavern!"
    );
}