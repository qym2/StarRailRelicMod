namespace StarRailRelic.Content.Items.Weapons.Melee
{
    public class BaseballBat : ModItem
    {
        public static int TotalBouncedDamage { get; set; } = 0;
        public static bool CanReflect { get; set; } = true;

        private static int bounceCooldownTimer = 0;

        public override void SetDefaults()
        {
            Item.SetWeaponValues(15, 4);

            Item.useTime = 22;
            Item.useAnimation = 22;

            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;

            Item.width = 56;
            Item.height = 56;

            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = RarityType<PurpleRarity>();
        }

        public override void UpdateInventory(Player player)
        {
            if (!CanReflect)
            {
                bounceCooldownTimer++;
                if (bounceCooldownTimer >= 600)
                {
                    CanReflect = true;
                    bounceCooldownTimer = 0;
                    TotalBouncedDamage = 0;
                }
            }
        }

        // TODO: 删除配方
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Diamond, 3)
                .AddRecipeGroup("GoldBar", 3)
                .AddIngredient(ItemID.Obsidian, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
