using TextAdventure.Items;

namespace TextAdventure
{
    public static class NPCDefinitions
    {
        public static FriendlyNPC Blacksmith;
        public static void InitNPCs()
        {
            Blacksmith = new FriendlyNPC(
                "Blacksmith",
                "A tall man with an incredibly hairy beard. Wears nothing but Pants and a leather apron",
                40,
                "Greetings traveler! Care to look at my wares?"
            );
            Blacksmith.Inventory.Add(Armor.ChainmailArmor);
            Blacksmith.Inventory.Add(Weapon.IronSword);
            Blacksmith.Inventory.Add(Healthing.LifePotion);
            Blacksmith.Inventory.Add(Healing.HealingHerbs);
            Blacksmith.Inventory.Add(Healing.HealingHerbs);
        }
    }
}
