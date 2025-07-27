namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class WorldbreakerBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 1, 0, 0);

            Item.rare = RarityType<PurpleRarity>();

            Item.width = 58;
            Item.height = 54;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe()
                .AddIngredient<LifelessBlade>(3)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
