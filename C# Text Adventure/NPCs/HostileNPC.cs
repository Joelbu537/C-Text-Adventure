namespace TextAdventure.NPCs;
public class HostileNPC : NPC
{
    public int Damage { get; private init; }
    public int Health { get; set; }
    public string? UnlockRoom {get; init;}
    public HostileNPC(string name, string description ,int money, int damage, int health, string dialogue = "They simply stare at you.", string? unlockRoom = null) : base(name, description, money, dialogue)
    {
        Damage = damage;
        Health = health;
        UnlockRoom = unlockRoom;
    }
}