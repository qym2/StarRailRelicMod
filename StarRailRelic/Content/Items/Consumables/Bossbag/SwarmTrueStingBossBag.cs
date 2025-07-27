namespace StarRailRelic.Content.Items.Consumables.Bossbag
{
    public class SwarmTrueStingBossBag : ModBossBag
    {
        public override bool PreHardmode => true;

        public override int Rare => ItemRarityID.Orange;

        public override void ModifyItemLoot(ItemLoot itemLoot) => SetBossBagItemLoot<SwarmTrueSting>(itemLoot);
    }
}
