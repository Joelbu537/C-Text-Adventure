namespace TextAdventure.NPCs;
public class NPC : ICloneable
{
    public string Name { get; private init; }
    public int Money { get; set; }
    public string Description { get; private init; }
    public string Dialogue { get; set; }
    public string MoneyText
    {
        get
        {
            return Color.FORE_WHITE + '$' + Color.FORE_GREEN + Money.ToString() + Color.RESET;
        }
    }
    public NPC(string name, string description, int money, string dialogue = "They do not respond.")
    {
        Name = name;
        Description = description;
        Money = money;
        Dialogue = dialogue;
    }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}