namespace StarRailRelic.Content.Items.Consumables.BossSummonItem
{
    public class HoneycorruptGrub : ModBossSummonItem
    {
        public override List<int> BossTypes => [NPCType<SwarmTrueSting>()];

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 34;
            Item.height = 38;

            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            _ = CreateRecipe()
                .AddIngredient<SwarmgnawedCarapace>(3)
                .AddIngredient(ItemID.Abeemination)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
