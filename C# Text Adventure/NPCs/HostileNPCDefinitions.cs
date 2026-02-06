namespace TextAdventure.NPCs;
public static class HostileNPCDefinitions
{
    public static HostileNPC Skeleton;

    static HostileNPCDefinitions()
    {
        Skeleton = new HostileNPC(
            name: "Skeleton",
            description: "A skeleton with a sword and a shield. It is screaming and banging its weapons against each other.",
            money: 20,
            damage: 4,
            health: 25,
            dialogue: "Rrrraaaaaahhhhhhhh!!! *Bang Bang Bang Bang*",
            unlockRoom: "Basement"
        );
    }
}