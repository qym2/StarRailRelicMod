namespace StarRailRelic.Content.Items
{
    public class RelicGuide : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = RarityType<BlueRarity>();
            Item.value = Item.sellPrice(0, 0, 12, 0);

            Item.width = 40;
            Item.height = 38;

            Item.useTime = 30;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            RelicDisplayUISystem uiSystem = GetInstance<RelicDisplayUISystem>();
            if (player.altFunctionUse == 2)
            {
                uiSystem.HideUI();
                return false;
            }
            if (uiSystem.isUIOpen)
            {
                uiSystem.HideUI();
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            GetInstance<RelicDisplayUISystem>().ShowUI();

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient<LostCrystal>(100)
                .Register();
        }
    }
}
