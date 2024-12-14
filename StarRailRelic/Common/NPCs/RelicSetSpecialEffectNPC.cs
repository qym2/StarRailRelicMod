using StarRailRelic.Common.EntitySource;

namespace StarRailRelic.Common.NPCs
{
    public class RelicSetSpecialEffectNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if(npc.value > 0 && !npc.friendly && !npc.dontTakeDamage && !npc.SpawnedFromStatue)
            {
                if(npc.GetSource_NPCKilledBy() is EntitySource_NPCKilledBy entitySource)
                {
                    Player player = entitySource.Attacker;
                    if (player != null)
                    {
                        RelicSetSpecialEffectPlayer modPlayer = player.GetModPlayer<RelicSetSpecialEffectPlayer>();
                        if (player.active && !player.dead && modPlayer.IsSigoniaTwoSet)
                        {
                            if (modPlayer.sigoniaBoostStacks < RelicSetSpecialEffectPlayer.sigoniaBoostMaxStacks)
                            {
                                modPlayer.sigoniaBoostStacks++;
                            }
                            modPlayer.sigoniaBoostTimer = RelicSetSpecialEffectPlayer.sigoniaBoostDuration;
                        }
                    }
                }
            }
        }
    }
}