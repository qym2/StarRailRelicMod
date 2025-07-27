namespace StarRailRelic.Content.Items.Tiles
{
    public class RelicStrengtheningTableItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(TileType<RelicStrengtheningTable>());

            Item.width = 32;
            Item.height = 28;

            Item.value = Item.sellPrice(0, 1, 65, 0);
            Item.rare = RarityType<PurpleRarity>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<LostCrystal>()
                .AddRecipeGroup("SilverBar", 10)
                .AddIngredient(ItemID.Diamond, 1)
                .AddIngredient(ItemID.Sapphire, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
