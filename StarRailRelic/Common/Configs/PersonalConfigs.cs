namespace StarRailRelic.Common.Configs
{
    public class PersonalConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static PersonalConfigs Instance => GetInstance<PersonalConfigs>();

        [DefaultValue(false)]
        public bool HideRelicButtonUI;

        [DefaultValue(true)]
        public bool HideRelicMainUIWithInventory;
    }
}
