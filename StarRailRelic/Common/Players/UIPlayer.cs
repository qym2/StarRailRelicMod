namespace StarRailRelic.Common.Players
{
    public class UIPlayer : ModPlayer
    {
        public override void SetControls()
        {
            if (GetInstance<RelicStrengtheningUISystem>().isUIOpen)
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
