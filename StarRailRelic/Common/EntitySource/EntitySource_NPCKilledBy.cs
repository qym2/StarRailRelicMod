namespace StarRailRelic.Common.EntitySource
{
    public class EntitySource_NPCKilledBy : EntitySource_Parent, IEntitySource
    {
        public Player Attacker { get; }

        public Entity Victim => Entity;

#nullable enable
        public EntitySource_NPCKilledBy(Entity entity, string? context = null) : base(entity, context)
        {
            if (Victim is NPC npc)
            {
                if (npc.lastInteraction == 255)
                {
                    Attacker = null;
                }
                else
                {
                    Attacker = Main.player[npc.lastInteraction];
                }
            }
        }
    }
}
