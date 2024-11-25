namespace StarRailRelic.Common.UI
{
    public class RelicButton : ModButtonUI
    {
        public override void UpdateUI()
        {
            DrawPosition = new(580, 255);
            TexturePath = "RelicButton";

            if (IsMouseHovering)
            {
                TexturePath = "RelicButton_Hover";
                UICommon.TooltipMouseText(RelicButtonText);
            }

            GetInstance<RelicMainUISystem>().uiState.SyncRelicData();
        }

        public override void OnLeftButtonClicked()
        {
            if (!GetInstance<RelicMainUISystem>().isUIOpen)
            {
                GetInstance<RelicMainUISystem>().ShowUI();
            }
            else
            {
                GetInstance<RelicMainUISystem>().HideUI();
            }
        }
    }
}
