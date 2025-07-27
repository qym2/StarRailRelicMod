namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class ShatteredBlade : ModItem
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

            Item.width = 36;
            Item.height = 36;
        }
    }
}
