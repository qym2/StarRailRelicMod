namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class AncestralHymn : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 0, 33, 0);

            Item.rare = RarityType<BlueRarity>();

            Item.width = 56;
            Item.height = 56;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe()
                .AddIngredient<HarmonicTune>(3)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
