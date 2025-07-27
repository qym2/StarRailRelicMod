namespace StarRailRelic.Common.NPCs
{
    public class ShatterDebuffNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool isDebuff;

        public override void ResetEffects(NPC npc)
        {
            isDebuff = false;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (isDebuff)
            {
                modifiers.Defense *= ShatterDebuffI.DefenseMultiplier;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (isDebuff)
            {
                drawColor.G = 0;
            }
        }
    }
}
