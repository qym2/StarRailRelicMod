namespace StarRailRelic.Content.Items.Consumables
{
    public class Fuel : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.maxStack = 20;

            Item.width = this.GetTextureValue().Width;
            Item.height = this.GetTextureValue().Height;

            Item.value = Item.sellPrice(0, 9, 0, 0);
            Item.rare = RarityType<PurpleRarity>();
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.GetModPlayer<TrailblazePowerPlayer>().TrailblazePower += 60;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldCoin, 9)
                .AddTile(TileID.AlchemyTable)
                .Register();
        }
    }
}
