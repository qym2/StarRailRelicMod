namespace StarRailRelic.Content.Items
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

            Item.width = this.GetTextureValue().Width;
            Item.height = this.GetTextureValue().Height;

            Item.rare = RarityType<GoldRarity>();
            Item.value = Item.sellPrice(0, 0, 1, 20);
        }

        public override bool CanUseItem(Player player)
        {
            EnvironmentPlayer envPlayer = player.GetModPlayer<EnvironmentPlayer>();
            SubworldPlayer subworldPlayer = player.GetModPlayer<SubworldPlayer>();
            TrailblazePowerPlayer powPlayer = player.GetModPlayer<TrailblazePowerPlayer>();

            if (powPlayer.TrailblazePower >= 40
                && (IsOutRelic ? envPlayer.ObtainableRelicsKeyOut != null : (envPlayer.ObtainableRelicsKeyIn != null && Main.hardMode))
                && !SubworldSystem.IsActive<DuelsLand>())
            {
                powPlayer.TrailblazePower -= 40;
                envPlayer.isOutRelic = IsOutRelic;
                subworldPlayer.oldPosition = player.position;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool? UseItem(Player player)
        {
            if (!player.GetModPlayer<SubworldPlayer>().InSubworld && player.itemAnimation == 5)
            {
                Item.stack -= 1;
                SubworldSystem.Enter<DuelsLand>();
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
}
