namespace StarRailRelic.Content.Items.Accessories
{
    public class StarRailCursor : ModItem
    {
        public override void SetDefaults()
        {
            Item.accessory = true;

            Item.width = 20;
            Item.height = 24;

            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = RarityType<PurpleRarity>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CommonAccessoryPlayer>().IsStarRailCursor = true;
        }

        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<CommonAccessoryPlayer>().IsStarRailCursor = true;
        }

        // TODO: 删除配方
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("SilverBar", 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
