namespace StarRailRelic.Content.Items.Consumables.BossSummonItem
{
    public class KeyOfFragmentum : ModBossSummonItem
    {
        public override List<int> BossTypes => [NPCType<IceOutOfSpace>(), NPCType<BlazeOutOfSpace>()];

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
                .AddIngredient<ShatteredBlade>(2)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ItemID.Gel, 20)
                .AddIngredient(ItemID.Torch, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
