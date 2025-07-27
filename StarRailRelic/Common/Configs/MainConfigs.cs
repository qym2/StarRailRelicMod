namespace StarRailRelic.Common.Configs
{
    public class MainConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static MainConfigs Instance => GetInstance<MainConfigs>();

        [Header("Game")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool SimplifiedMode;
    }
}
