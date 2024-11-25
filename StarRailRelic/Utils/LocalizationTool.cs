namespace StarRailRelic.Utils
{
    public static class LocalizationTool
    {
        public static class UI
        {
            /// <summary>
            /// 装备遗器按钮文字
            /// </summary>
            public static string RelicButtonText => GetTextValue("Mods.StarRailRelic.UI.RelicButton");
            /// <summary>
            /// 头部遗器栏位文字
            /// </summary>
            public static string HeadRelicText => GetTextValue("Mods.StarRailRelic.UI.HeadRelicText");
            /// <summary>
            /// 手部遗器栏位文字
            /// </summary>
            public static string HandsRelicText => GetTextValue("Mods.StarRailRelic.UI.HandsRelicText");
            /// <summary>
            /// 躯干遗器栏位文字
            /// </summary>
            public static string BodyRelicText => GetTextValue("Mods.StarRailRelic.UI.BodyRelicText");
            /// <summary>
            /// 脚部遗器栏位文字
            /// </summary>
            public static string FeetRelicText => GetTextValue("Mods.StarRailRelic.UI.FeetRelicText");
            /// <summary>
            /// 位面球遗器栏位文字
            /// </summary>
            public static string PlanarSphereRelicText => GetTextValue("Mods.StarRailRelic.UI.PlanarSphereRelicText");
            /// <summary>
            /// 连结绳遗器栏位文字
            /// </summary>
            public static string LinkRopeRelicText => GetTextValue("Mods.StarRailRelic.UI.LinkRopeRelicText");

            /// <summary>
            /// 遗器强化文字
            /// </summary>
            public static string RelicStrengtheningText => GetTextValue("Mods.StarRailRelic.UI.RelicStrengtheningText");
            /// <summary>
            /// 遗器强化材料不足提示文字
            /// </summary>
            public static string RelicCannotStrengtheningText => GetTextValue("Mods.StarRailRelic.UI.RelicCannotStrengtheningText");
            /// <summary>
            /// 请放入遗器提示文字
            /// </summary>
            public static string PlaceRelicText => GetTextValue("Mods.StarRailRelic.UI.PlaceRelicText");
            /// <summary>
            /// 遗器已满级提示文字
            /// </summary>
            public static string RelicLevelMaxText => GetTextValue("Mods.StarRailRelic.UI.RelicLevelMaxText");
            /// <summary>
            /// 遗器下一级所需材料提示文字
            /// </summary>
            public static LocalizedText NextLevelItemCountText => GetText("Mods.StarRailRelic.UI.NextLevelItemCountText");
            /// <summary>
            /// 遗器下一级所需材料提示文字
            /// </summary>
            public static string UpgradeCompletedText => GetTextValue("Mods.StarRailRelic.UI.UpgradeCompletedText");
            /// <summary>
            /// 遗器信息提示文字
            /// </summary>
            public static LocalizedText RelicInforText => GetText("Mods.StarRailRelic.UI.RelicInforText");
            /// <summary>
            /// 遗器信息提示文字
            /// </summary>
            public static string TrailblazePowerTipText => GetTextValue("Mods.StarRailRelic.UI.TrailblazePowerTipText");
            /// <summary>
            /// 遗器获取提示文字
            /// </summary>
            public static LocalizedText StarRailPassTip => GetText("Mods.StarRailRelic.Items.StarRailPassTip");

            /// <summary>
            /// 主词条本地化
            /// </summary>
            public static List<LocalizedText> MainEntryText =>
            [
                GetText("Mods.StarRailRelic.UI.Relic.LifeFlat"),
                GetText("Mods.StarRailRelic.UI.Relic.DamageFlat"),
                GetText("Mods.StarRailRelic.UI.Relic.LifeAdditive"),
                GetText("Mods.StarRailRelic.UI.Relic.DamageAdditive"),
                GetText("Mods.StarRailRelic.UI.Relic.Endurance"),
                GetText("Mods.StarRailRelic.UI.Relic.CritChance"),
                GetText("Mods.StarRailRelic.UI.Relic.LifeRegen"),
                GetText("Mods.StarRailRelic.UI.Relic.MeleeAttackSpeed"),
                GetText("Mods.StarRailRelic.UI.Relic.MeleeArmorPenetration"),
                GetText("Mods.StarRailRelic.UI.Relic.RangedArmorPenetration"),
                GetText("Mods.StarRailRelic.UI.Relic.MagicArmorPenetration"),
                GetText("Mods.StarRailRelic.UI.Relic.SummonArmorPenetration"),
                GetText("Mods.StarRailRelic.UI.Relic.ManaCostReduction"),
                GetText("Mods.StarRailRelic.UI.Relic.MaxMinions"),
                GetText("Mods.StarRailRelic.UI.Relic.Defense")
            ];

            /// <summary>
            /// 副词条本地化
            /// </summary>
            public static List<LocalizedText> AdverbEntryText =>
            [
                GetText("Mods.StarRailRelic.UI.Relic.LifeFlat"),
                GetText("Mods.StarRailRelic.UI.Relic.LifeAdditive"),
                GetText("Mods.StarRailRelic.UI.Relic.DamageAdditive"),
                GetText("Mods.StarRailRelic.UI.Relic.CritChance"),
                GetText("Mods.StarRailRelic.UI.Relic.LifeRegen"),
                GetText("Mods.StarRailRelic.UI.Relic.MeleeAttackSpeed"),
                GetText("Mods.StarRailRelic.UI.Relic.ArmorPenetration"),
                GetText("Mods.StarRailRelic.UI.Relic.ManaCostReduction"),
                GetText("Mods.StarRailRelic.UI.Relic.Defense")
            ];
        }

        public static class Terraria
        {
            /// <summary>
            /// “任何”
            /// </summary>
            public static string Any => GetTextValue("LegacyMisc.37");
            /// <summary>
            /// “选择”
            /// </summary>
            public static string Select => GetTextValue("LegacyMisc.53");
            /// <summary>
            /// “拿取”
            /// </summary>
            public static string Take => GetTextValue("LegacyMisc.54");
            /// <summary>
            /// “拿取一个”
            /// </summary>
            public static string TakeOne => GetTextValue("LegacyMisc.55");
            /// <summary>
            /// “关闭”
            /// </summary>
            public static string Close => GetTextValue("LegacyMisc.56");
        }
    }
}
