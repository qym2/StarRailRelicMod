namespace StarRailRelic.Utils
{
    /// <summary>
    /// 遗器部位
    /// </summary>
    public enum RelicType
    {
        /// <summary>
        /// 头部
        /// </summary>
        Head,
        /// <summary>
        /// 手部
        /// </summary>
        Hands,
        /// <summary>
        /// 躯干
        /// </summary>
        Body,
        /// <summary>
        /// 脚部
        /// </summary>
        Feet,
        /// <summary>
        /// 位面球
        /// </summary>
        PlanarSphere,
        /// <summary>
        /// 连结绳
        /// </summary>
        LinkRope,
        /// <summary>
        /// 无
        /// </summary>
        None
    }

    /// <summary>
    /// 主词条种类
    /// </summary>
    public enum RelicMainEntryType
    {
        /// <summary>
        /// 最终生命值提高
        /// </summary>
        LifeFlat,
        /// <summary>
        /// 最终伤害提高
        /// </summary>
        DamageFlat,
        /// <summary>
        /// 生命值百分比提高
        /// </summary>
        LifeAdditive,
        /// <summary>
        /// 伤害百分比提高
        /// </summary>
        DamageAdditive,
        /// <summary>
        /// 免伤提高
        /// </summary>
        Endurance,
        /// <summary>
        /// 暴击率提高
        /// </summary>
        CritChance,
        /// <summary>
        /// 生命再生速率提高
        /// </summary>
        LifeRegen,
        /// <summary>
        /// 近战速度提高
        /// </summary>
        MeleeAttackSpeed,
        /// <summary>
        /// 近战伤害盔甲穿透提高
        /// </summary>
        MeleeArmorPenetration,
        /// <summary>
        /// 远程伤害盔甲穿透提高
        /// </summary>
        RangedArmorPenetration,
        /// <summary>
        /// 魔法伤害盔甲穿透提高
        /// </summary>
        MagicArmorPenetration,
        /// <summary>
        /// 召唤伤害盔甲穿透提高
        /// </summary>
        SummonArmorPenetration,
        /// <summary>
        /// 魔力消耗降低
        /// </summary>
        ManaCostReduction,
        /// <summary>
        /// 召唤物上限提高
        /// </summary>
        MaxMinions,
        /// <summary>
        /// 防御力提高
        /// </summary>
        Defense
    }

    /// <summary>
    /// 副词条种类
    /// </summary>
    public enum RelicAdverbEntryType
    {
        /// <summary>
        /// 最终生命值提高
        /// </summary>
        LifeFlat,
        /// <summary>
        /// 生命值百分比提高
        /// </summary>
        LifeAdditive,
        /// <summary>
        /// 伤害百分比提高
        /// </summary>
        DamageAdditive,
        /// <summary>
        /// 暴击率提高
        /// </summary>
        CritChance,
        /// <summary>
        /// 生命再生速率提高
        /// </summary>
        LifeRegen,
        /// <summary>
        /// 近战速度提高
        /// </summary>
        MeleeAttackSpeed,
        /// <summary>
        /// 盔甲穿透提高
        /// </summary>
        ArmorPenetration,
        /// <summary>
        /// 魔力消耗降低
        /// </summary>
        ManaCostReduction,
        /// <summary>
        /// 防御力提高
        /// </summary>
        Defense
    }

    /// <summary>
    /// 遗器套装
    /// </summary>
    public enum RelicSet
    {
        /// <summary>
        /// 繁星璀璨的天才
        /// </summary>
        Quantum,
        /// <summary>
        /// 停转的萨尔索图
        /// </summary>
        Salsotto,
        /// <summary>
        /// 戍卫风雪的铁卫
        /// </summary>
        Guard,
        /// <summary>
        /// 太空封印站
        /// </summary>
        Space,
        /// <summary>
        /// 不老者的仙舟
        /// </summary>
        Xianzhou,
        /// <summary>
        /// 泛银河商业公司
        /// </summary>
        Enterprise,
        /// <summary>
        /// 筑城者的贝洛伯格
        /// </summary>
        Belobog,
        /// <summary>
        /// 星体差分机
        /// </summary>
        Differentiator,
        /// <summary>
        /// 盗贼公国塔利亚
        /// </summary>
        Banditry,
        /// <summary>
        /// 生命的翁瓦克
        /// </summary>
        Vonwacq,
        /// <summary>
        /// 繁星竞技场
        /// </summary>
        Rutilant,
        /// <summary>
        /// 折断的龙骨
        /// </summary>
        Keel,
        /// <summary>
        /// 苍穹战线格拉默
        /// </summary>
        Glamoth,
        /// <summary>
        /// 梦想之地匹诺康尼
        /// </summary>
        Penacony,
        /// <summary>
        /// 无主荒星茨冈尼亚
        /// </summary>
        Sigonia,
        /// <summary>
        /// 出云显世与高天神国
        /// </summary>
        Izumo,
        /// <summary>
        /// 奔狼的都蓝王朝
        /// </summary>
        Wolves,
        /// <summary>
        /// 劫火莲灯铸炼宫
        /// </summary>
        Kalpagni,
        /// <summary>
        /// 云无留迹的过客
        /// </summary>
        Healing,
        /// <summary>
        /// 野穗伴行的快枪手
        /// </summary>
        Musketeer,
        /// <summary>
        /// 净庭教宗的圣骑士
        /// </summary>
        Knight,
        /// <summary>
        /// 密林卧雪的猎人
        /// </summary>
        Ice,
        /// <summary>
        /// 街头出身的拳王
        /// </summary>
        Physics,
        /// <summary>
        /// 熔岩锻铸的火匠
        /// </summary>
        Fire,
        /// <summary>
        /// 激奏雷电的乐队
        /// </summary>
        Lightning,
        /// <summary>
        /// 晨昏交界的翔鹰
        /// </summary>
        Wind,
        /// <summary>
        /// 流星追迹的怪盗
        /// </summary>
        Thief,
        /// <summary>
        /// 盗匪荒漠的废土客
        /// </summary>
        Imaginary,
        /// <summary>
        /// 宝命长存的莳者
        /// </summary>
        Life,
        /// <summary>
        /// 骇域漫游的信使
        /// </summary>
        Messenger,
        /// <summary>
        /// 毁烬焚骨的大公
        /// </summary>
        Duke,
        /// <summary>
        /// 幽锁深牢的系囚
        /// </summary>
        Dot,
        /// <summary>
        /// 死水深潜的先驱
        /// </summary>
        DeadWaters,
        /// <summary>
        /// 机心戏梦的钟表匠
        /// </summary>
        Watch,
        /// <summary>
        /// 荡除蠹灾的铁骑
        /// </summary>
        Iron,
        /// <summary>
        /// 风举云飞的勇烈
        /// </summary>
        Feixiao,
        /// <summary>
        /// 沉陆海域露莎卡
        /// </summary>
        Sunken,
        /// <summary>
        /// 奇想蕉乐园
        /// </summary>
        Banana,
        /// <summary>
        /// 重循苦旅的司铎
        /// </summary>
        Sacerdos,
        /// <summary>
        /// 识海迷坠的学者
        /// </summary>
        Scholar
    }

    public enum PlayerEnvironment
    {
        /// <summary>
        /// 海洋
        /// </summary>
        Beach,

        /// <summary>
        /// 腐化
        /// </summary>
        Corrupt,

        /// <summary>
        /// 猩红
        /// </summary>
        Crimson,

        /// <summary>
        /// 沙漠
        /// </summary>
        Desert,

        /// <summary>
        /// 泥土层
        /// </summary>
        DirtLayerHeight,

        /// <summary>
        /// 地牢
        /// </summary>
        Dungeon,

        /// <summary>
        /// 森林
        /// </summary>
        Forest,

        /// <summary>
        /// 宝石洞
        /// </summary>
        GemCave,

        /// <summary>
        /// 发光蘑菇
        /// </summary>
        Glowshroom,

        /// <summary>
        /// 花岗岩
        /// </summary>
        Granite,

        /// <summary>
        /// 墓地
        /// </summary>
        Graveyard,

        /// <summary>
        /// 神圣
        /// </summary>
        Hallow,

        /// <summary>
        /// 蜂巢
        /// </summary>
        Hive,

        /// <summary>
        /// 丛林
        /// </summary>
        Jungle,

        /// <summary>
        /// 丛林蜥蜴神庙
        /// </summary>
        LihzhardTemple,

        /// <summary>
        /// 大理石
        /// </summary>
        Marble,

        /// <summary>
        /// 陨石
        /// </summary>
        Meteor,

        /// <summary>
        /// 普通洞穴
        /// </summary>
        NormalCaverns,

        /// <summary>
        /// 普通太空
        /// </summary>
        NormalSpace,

        /// <summary>
        /// 普通地下
        /// </summary>
        NormalUnderground,

        /// <summary>
        /// 旧日军团
        /// </summary>
        OldOneArmy,

        /// <summary>
        /// 地上
        /// </summary>
        OverworldHeight,

        /// <summary>
        /// 和平蜡烛
        /// </summary>
        PeaceCandle,

        /// <summary>
        /// 纯净
        /// </summary>
        Purity,

        /// <summary>
        /// 雨天
        /// </summary>
        Rain,

        /// <summary>
        /// 岩石层
        /// </summary>
        RockLayerHeight,

        /// <summary>
        /// 沙尘暴
        /// </summary>
        Sandstorm,

        /// <summary>
        /// 暗影蜡烛
        /// </summary>
        ShadowCandle,

        /// <summary>
        /// 微光
        /// </summary>
        Shimmer,

        /// <summary>
        /// 太空
        /// </summary>
        SkyHeight,

        /// <summary>
        /// 雪原
        /// </summary>
        Snow,

        /// <summary>
        /// 星云柱
        /// </summary>
        TowerNebula,

        /// <summary>
        /// 日耀柱
        /// </summary>
        TowerSolar,

        /// <summary>
        /// 星尘柱
        /// </summary>
        TowerStardust,

        /// <summary>
        /// 星旋柱
        /// </summary>
        TowerVortex,

        /// <summary>
        /// 地下沙漠
        /// </summary>
        UndergroundDesert,

        /// <summary>
        /// 地狱
        /// </summary>
        UnderworldHeight,

        /// <summary>
        /// 水蜡烛
        /// </summary>
        WaterCandle
    }

}
