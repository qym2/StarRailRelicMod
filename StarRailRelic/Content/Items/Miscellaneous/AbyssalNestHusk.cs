namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class AbyssalNestHusk : ModItem
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

            Item.width = 60;
            Item.height = 64;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe()
                .AddIngredient<SwarmgnawedCarapace>(3)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
