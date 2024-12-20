namespace StarRailRelic.Common.Players
{
    public class KeybindPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.RelicButtonKeybind.JustPressed && !PersonalConfigs.Instance.HideRelicButtonUI)
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
}
