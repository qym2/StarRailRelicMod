namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class SwarmgnawedCarapace : ModItem
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

            Item.width = 46;
            Item.height = 50;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe()
                .AddIngredient<RottenStingcarapace>(3)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
