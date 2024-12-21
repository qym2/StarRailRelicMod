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
            Item.maxStack = 999999;

            Item.value = Item.sellPrice(0, 0, 0, 12);

            Item.rare = RarityType<BlueRarity>();

            Item.width = 33;
            Item.height = 30;
        }

        public override void UpdateInventory(Player player)
        {
            Item.maxStack = 999999;
        }

        public override void PostUpdate()
        {
            Item.maxStack = 999999;
        }
    }
}
