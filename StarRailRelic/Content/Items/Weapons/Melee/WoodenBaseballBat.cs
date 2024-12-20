namespace StarRailRelic.Content.Items.Weapons.Melee
{
    public class WoodenBaseballBat : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetWeaponValues(9, 4);

            Item.useTime = 22;
            Item.useAnimation = 22;

            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;

            Item.width = 54;
            Item.height = 54;

            Item.value = Item.sellPrice(0, 0, 0, 45);
            Item.rare = RarityType<GreenRarity>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
