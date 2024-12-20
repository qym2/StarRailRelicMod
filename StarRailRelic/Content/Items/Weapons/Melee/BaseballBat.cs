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

            Item.value = Item.sellPrice(0, 5, 50, 0);
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<WoodenBaseballBat>()
                .AddIngredient<WeaponChargingComponent>()
                .AddIngredient(ItemID.Diamond, 4)
                .AddRecipeGroup("GoldBar", 2)
                .AddIngredient(ItemID.Obsidian, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
