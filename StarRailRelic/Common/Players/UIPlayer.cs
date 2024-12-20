namespace StarRailRelic.Common.Players
{
    public class UIPlayer : ModPlayer
    {
        public override void SetControls()
        {
            bool configCondition1 = (!PersonalConfigs.Instance.HideRelicButtonUI && !PersonalConfigs.Instance.HideRelicMainUIWithInventory) || PersonalConfigs.Instance.HideRelicButtonUI;
            bool configCondition2 = !PersonalConfigs.Instance.HideRelicButtonUI && PersonalConfigs.Instance.HideRelicMainUIWithInventory;
            
            bool uiCondition1 = GetInstance<RelicStrengtheningUISystem>().isUIOpen;
            bool uiCondition2 = GetInstance<RelicStrengtheningUISystem>().isUIOpen || GetInstance<RelicMainUISystem>().isUIOpen;

            bool condition = (configCondition1 && uiCondition1) || (configCondition2 && uiCondition2);

            if (condition)
            {
                if (Main.LocalPlayer.controlInv)
                {
                    GetInstance<RelicStrengtheningUISystem>().HideUI();
                    GetInstance<RelicMainUISystem>().HideUI();
                    Main.LocalPlayer.releaseInventory = false;
                    Main.playerInventory = false;
                }
                else if (Main.LocalPlayer.controlCreativeMenu)
                {
                    GetInstance<RelicStrengtheningUISystem>().HideUI();
                    GetInstance<RelicMainUISystem>().HideUI();
                    Main.LocalPlayer.releaseCreativeMenu = false;
                    Main.playerInventory = false;
                }
            }
        }
    }
}
