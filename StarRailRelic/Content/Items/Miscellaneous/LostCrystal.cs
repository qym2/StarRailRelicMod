namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class LostCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10000;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 0, 12, 0);

            Item.rare = RarityType<BlueRarity>();

            Item.width = 33;
            Item.height = 30;
        }
    }
}
