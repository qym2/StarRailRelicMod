/*namespace StarRailRelic.Content.Items
{
    public class StarRailPass : ModItem
    {
        public virtual bool IsOutRelic => true;

        public override void SetDefaults()
        {
            Item.maxStack = 20;

            Item.useAnimation = 30;
            Item.useTime = 30;

            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.width = 38;
            Item.height = 38;

            Item.rare = RarityType<GoldRarity>();
            Item.value = Item.sellPrice(0, 0, 1, 20);
        }

        public override bool CanUseItem(Player player)
        {
            EnvironmentPlayer envPlayer = player.GetModPlayer<EnvironmentPlayer>();
            TrailblazePowerPlayer powPlayer = player.GetModPlayer<TrailblazePowerPlayer>();

            if (powPlayer.TrailblazePower >= 40)
            {
                powPlayer.TrailblazePower -= 40;
                envPlayer.isOutRelic = IsOutRelic;

                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool? UseItem(Player player)
        {
            Item.stack -= 1;
            int[] relicTypes = player.GetModPlayer<EnvironmentPlayer>().isOutRelic
                ? ([.. ModRelic.ModOutRelicLists[(RelicSet)player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyOut].Values])
                : ([.. ModRelic.ModInRelicLists[(RelicSet)player.GetModPlayer<EnvironmentPlayer>().ObtainableRelicsKeyIn].Values]);

            int relicNum = MainConfigs.Instance.SimplifiedMode ? 4 : 2;

            for (int i = 0; i < relicNum; i++)
            {
                int type = relicTypes[Main.rand.Next(relicTypes.Length)];
                NewItemSycn(new EntitySource_Gift(player), player.position, type);
            }
            return null;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            EnvironmentPlayer envPlayer = player.GetModPlayer<EnvironmentPlayer>();

            string environments = "";
            if (envPlayer.Environments != null)
            {
                environments = string.Join(", ", envPlayer.Environments
                    .Select(env => GetTextValue($"Mods.StarRailRelic.Environment.Zone{env}")));
            }

            string relics = "";
            if ((IsOutRelic ? ModRelic.ModOutRelicLists : ModRelic.ModInRelicLists) != null &&
                (IsOutRelic ? envPlayer.ObtainableRelicsKeyOut : envPlayer.ObtainableRelicsKeyIn) != null)
            {
                relics = string.Join("  ", (IsOutRelic ? ModRelic.ModOutRelicLists : ModRelic.ModInRelicLists)
                    [(RelicSet)(IsOutRelic ? envPlayer.ObtainableRelicsKeyOut : envPlayer.ObtainableRelicsKeyIn)]
                    .Select(pair => $"{GetText($"Mods.StarRailRelic.ItemTextrue").WithFormatArgs(pair.Key)}"));
            }

            string starRailPassTip = StarRailPassTip.WithFormatArgs(environments, relics).Value;

            TooltipLine line = new(Mod, "StarRailPassTip", starRailPassTip)
            {
                OverrideColor = new Color(0, 255, 255)
            };
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<LostCrystal>(10)
                .Register();
        }
    }

    public class StarRailSpecialPass : StarRailPass
    {
        public override bool IsOutRelic => false;
    }
}*/
