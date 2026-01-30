using System;
using System.Collections.Generic;
using System.Text;

namespace C__Text_Adventure
{
    public static class NPCDefinitions
    {
        public static NPC Blacksmith;
        public static void InitNPCs()
        {
            Blacksmith = new FriendlyNPC(
                "Blacksmith",
                "A tall man with an incredibly hairy beard. Wears nothing but Pants and a leather apron",
                40
            );
        }
    }
}
