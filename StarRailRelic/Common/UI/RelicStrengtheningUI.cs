namespace StarRailRelic.Common.UI
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
            slot.SetRectangle(left: 580, top: 140, width: 64, height: 64);
            text.Top.Set(slot.Height.Pixels + 8, 0f);
            text.Left.Set(0f, 0f);
            slot.Append(text);
            Append(slot);

            button = new(RelicStrengtheningButtonTexture, RelicStrengtheningButton_HoverTexture);
            button.SetRectangle(left: 652, top: 172, width: 28, height: 24);
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
