namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class RelicRemians : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1000;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 0, 2, 0);

            Item.rare = RarityType<GoldRarity>();

            Item.width = 56;
            Item.height = 56;
        }
    }
}
