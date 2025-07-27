/*namespace StarRailRelic.Content.Items
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
                .AddIngredient<LostCrystal>()
                .Register();
        }
    }
}*/
namespace StarRailRelic.Common.UI.Relic
{
    [Autoload(Side = ModSide.Client)]
    public class RelicStrengtheningUISystem : UISystem<RelicStrengtheningUI>
    {
    }

    public class RelicStrengtheningUI : UIState
    {
        private readonly RelicUnitSlot slot = new(RelicType.None);
        private readonly UIText text = new("");
        private UIHoverImageButton button;

        public override void OnInitialize()
        {
            slot.SetRectangle(left: 580, top: 112, width: 64, height: 64);
            text.Top.Set(slot.Height.Pixels + 8, 0f);
            text.Left.Set(0f, 0f);
            slot.Append(text);
            Append(slot);

            button = new(RelicStrengtheningButtonTexture, RelicStrengtheningButton_HoverTexture);
            button.SetRectangle(left: 652, top: 144, width: 28, height: 24);
            button.OnLeftClickButton += ButtonClicked;
            Append(button);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // 更新栏位文字说明
            text.SetText(RelicStrengtheningText);
        }

        private void ButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            LevelUpRelic();
        }

        private void LevelUpRelic()
        {
            if (slot.relicItem.IsValidRelic(out ModRelic relic))
            {
                if(relic.level < relic.levelMax)
                {
                    int itemType = ItemType<LostCrystal>();
                    int itemCount = Main.LocalPlayer.CountPlayerItems(itemType);
                    int requiredItemCount = ModRelic.RelicStrengtheningRequiredItemCount[relic.level];

                    if (itemCount >= requiredItemCount)
                    {
                        Main.LocalPlayer.ConsumeItems(itemType, requiredItemCount);

                        relic.LevelUp();

                        SoundEngine.PlaySound(SoundID.Grab);

                        Main.NewText(UpgradeCompletedText, Color.YellowGreen);
                        if (relic.level < relic.levelMax)
                        {
                            Main.NewText(NextLevelItemCountText.WithFormatArgs(ModRelic.RelicStrengtheningRequiredItemCount[relic.level]), Color.YellowGreen);
                        }
                        else
                        {
                            Main.NewText(RelicLevelMaxText, Color.Red);
                        }
                    }
                    else
                    {
                        Main.NewText(RelicCannotStrengtheningText, Color.Red);
                    }
                }
                else
                {
                    Main.NewText(RelicLevelMaxText, Color.Red);
                }
            }
            else
            {
                Main.NewText(PlaceRelicText, Color.Red);
            }
        }
    }
}
