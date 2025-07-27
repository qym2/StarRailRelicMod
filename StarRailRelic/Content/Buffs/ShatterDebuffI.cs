namespace StarRailRelic.Content.Buffs
{
    public class ShatterDebuffI : ModBuff
    {
        public const int DefenseReductionPercent = 15;
        public const float DefenseMultiplier = 1 - DefenseReductionPercent / 100f;
        public override LocalizedText Description => base.Description.WithFormatArgs(DefenseReductionPercent);

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<ShatterDebuffNPC>().isDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShatterDebuffPlayer>().isDebuff = true;
            player.statDefense *= DefenseMultiplier;
        }
    }
}
