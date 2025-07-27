namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class HarmonicTune : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 0, 11, 0);

            Item.rare = RarityType<GreenRarity>();

            Item.width = 40;
            Item.height = 34;
        }
    }
}
