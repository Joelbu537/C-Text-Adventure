namespace TextAdventure.NPCs;
public static class HostileNPCDefinitions
{
    public static NPC Skeleton;
    public static NPC Spiderman;
    public static NPC Sahur;

    static HostileNPCDefinitions()
    {
        Skeleton = new HostileNPC(
            name: "Skeleton",
            description: "A skeleton with a sword and a shield. It is screaming and banging its weapons against each other.",
            money: 20,
            damage: 4,
            health: 25,
            dialogue: "\"Rrrraaaaaahhhhhhhh!!!\" *Bang Bang Bang Bang*",
            onDeath: () => { RoomDefinitions.Basement.IsUnlocked = true; }
        );
        Spiderman = new HostileNPC(
            name: "Spider Man",
            description: "A man in a red suit with a spider painted on it.",
            money: 40,
            damage: 14,
            health: 50,
            dialogue: "It's the guy from Fortnite..."
        );
        Sahur = new HostileNPC(
            name: "Tung Tung Tung Tung Tung Tung Tung Sahur",
            description: "A baseball bat with human features holding a baseball bat without human features.",
            money: 25,
            damage: 6,
            health: 30,
            dialogue: "\"Tung Tung Tung Tung Tung Tung Tung Sahur\""
        );
    }
}