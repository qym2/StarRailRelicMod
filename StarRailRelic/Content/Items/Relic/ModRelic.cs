namespace StarRailRelic.Content.Items.Relic
{
    /// <summary>
    /// 表示一个抽象的遗器类，所有遗器的类型应继承自此类。 
    /// 每个遗器有其类型、等级，主词条和副词条等属性。 
    /// 此类提供了有关遗器的等级提升、词条生成、UI提示信息以及数据保存和加载等功能。 
    /// </summary>
    public abstract class ModRelic : ModItem
    {
        #region 字段和属性
        /// <summary>
        /// 遗器的类型
        /// </summary>
        public abstract RelicType RelicType { get; }

        /// <summary>
        /// 遗器的套装
        /// </summary>
        public abstract RelicSet RelicSet { get; }

        /// <summary>
        /// 当前遗器的等级，初始为0
        /// </summary>
        public int level = 0;
        /// <summary>
        /// 最大等级，默认为15
        /// </summary>
        public int levelMax = 15;
        /// <summary>
        /// 遗器升每一级所需的物品数量列表
        /// </summary>
        public static readonly List<int> RelicStrengtheningRequiredItemCount =
            [56, 80, 104, 136, 168, 206, 264, 324, 412, 512, 635, 803, 1032, 1296, 1572];

        /// <summary>
        /// 主词条的类型
        /// </summary>
        public RelicMainEntryType? MainEntry;
        /// <summary>
        /// 主词条的加成数值列表（根据 <see cref="RelicMainEntryType"/> 排列）
        /// </summary>
        public static readonly List<float> MainEntryBonus =
        [
            5,// 最终生命值提高
            1,// 最终伤害提高
            1.2f,// 生命值百分比提高
            1.2f,// 伤害百分比提高
            1,// 免伤提高
            1.4f,// 暴击率提高
            0.4f,// 生命再生速率提高
            1.4f,// 近战攻击速度提高
            0.8f,// 近战伤害盔甲穿透提高
            0.8f,// 远程伤害盔甲穿透提高
            0.8f,// 魔法伤害盔甲穿透提高
            0.8f,// 召唤伤害盔甲穿透提高
            2,// 魔力消耗降低
            0.2f,// 召唤物上限提高
            1.2f// 防御力提高
        ];

        /// <summary>
        /// 每个副词条的类型列表
        /// </summary>
        public List<RelicAdverbEntryType> AdverbEntryList;
        /// <summary>
        /// 每个副词条的出现次数列表
        /// </summary>
        public List<int> AdverbEntryNumList;
        /// <summary>
        /// 副词条的加成数值列表（根据 <see cref="RelicMainEntryType"/> 排列）
        /// </summary>
        public static readonly List<float> AdverbEntryBonus =
        [
            6.7f,// 最终生命值提高
            1.6f,// 生命值百分比提高
            1.6f,// 伤害百分比提高
            1.9f,// 暴击率提高
            1.1f,// 生命再生速率提高
            1.9f,// 近战攻击速度提高
            1.1f,// 盔甲穿透提高
            2.7f,// 魔力消耗降低
            1.6f// 防御力提高
        ];

        /// <summary>
        /// 是否二件套
        /// </summary>
        public bool isTwoSet;
        /// <summary>
        /// 是否四件套
        /// </summary>
        public bool isFourSet;

        /// <summary>
        /// 是否是展示模式
        /// </summary>
        public bool isDisplay;

        /// <summary>
        /// 当副词条初始为3个时的待定副词条类型
        /// </summary>
        private RelicAdverbEntryType? pendingAdverbEntry;
        /// <summary>
        /// 主词条的抽取的池子
        /// </summary>
        private List<RelicMainEntryType> mainEntryPool;

        public static readonly Dictionary<RelicSet, Dictionary<string, int>> ModOutRelicLists = new()
        {
            {RelicSet.Feixiao, CreateRelicList("Feixiao", "Two")},
            {RelicSet.Sacerdos, CreateRelicList("Sacerdos", "Two")},
            {RelicSet.Watch, CreateRelicList("Watch", "Two")},
            {RelicSet.DeadWaters, CreateRelicList("DeadWaters", "Two")},
            {RelicSet.Dot, CreateRelicList("Dot", "Two")},
            {RelicSet.Quantum, CreateRelicList("Quantum", "One")},
            {RelicSet.Guard, CreateRelicList("Guard", "One")},
            {RelicSet.Ice, CreateRelicList("Ice", "One")},
            {RelicSet.Scholar, CreateRelicList("Scholar", "Two")},
            {RelicSet.Wind, CreateRelicList("Wind", "One")},
            {RelicSet.Imaginary, CreateRelicList("Imaginary", "One")},
            {RelicSet.Messenger, CreateRelicList("Messenger", "One")},
            {RelicSet.Life, CreateRelicList("Life", "One")},
            {RelicSet.Iron, CreateRelicList("Iron", "Two")},
            {RelicSet.Healing, CreateRelicList("Healing", "One")},
            {RelicSet.Knight, CreateRelicList("Knight", "One")},
            {RelicSet.Lightning, CreateRelicList("Lightning", "One")},
            {RelicSet.Duke, CreateRelicList("Duke", "Two")},
            {RelicSet.Fire, CreateRelicList("Fire", "One")},
            {RelicSet.Physics, CreateRelicList("Physics", "One")},
            {RelicSet.Thief, CreateRelicList("Thief", "One")},
            {RelicSet.Musketeer, CreateRelicList("Musketeer", "One")}
        };

        public static readonly Dictionary<RelicSet, Dictionary<string, int>> ModInRelicLists = new()
        {
            {RelicSet.Salsotto, CreateRelicList("Salsotto", "One", false)},
            {RelicSet.Differentiator, CreateRelicList("Differentiator", "One", false)},
            {RelicSet.Space, CreateRelicList("Space", "One", false)},
            {RelicSet.Enterprise, CreateRelicList("Enterprise", "One", false)},
            {RelicSet.Izumo, CreateRelicList("Izumo", "Two", false)},
            {RelicSet.Sigonia, CreateRelicList("Sigonia", "Two", false)},
            {RelicSet.Penacony, CreateRelicList("Penacony", "One", false)},
            {RelicSet.Belobog, CreateRelicList("Belobog", "One", false)},
            {RelicSet.Sunken, CreateRelicList("Sunken", "Two", false)},
            {RelicSet.Wolves, CreateRelicList("Wolves", "Two", false)},
            {RelicSet.Banana, CreateRelicList("Banana", "Two", false)},
            {RelicSet.Keel, CreateRelicList("Keel", "One", false)},
            {RelicSet.Glamoth, CreateRelicList("Glamoth", "One", false)},
            {RelicSet.Vonwacq, CreateRelicList("Vonwacq", "One", false)},
            {RelicSet.Kalpagni, CreateRelicList("Kalpagni", "Two", false)},
            {RelicSet.Rutilant, CreateRelicList("Rutilant", "One", false)},
            {RelicSet.Banditry, CreateRelicList("Banditry", "One", false)},
            {RelicSet.Xianzhou, CreateRelicList("Xianzhou", "One", false)},
        };
        #endregion

        #region 重写方法
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.rare = RarityType<GoldRarity>();

            Item.width = 45;
            Item.height = 45;

            SetMainEntryByRelicType();

            if (AdverbEntryList == null)
            {
                InitializeAdverbEntries();
            }

            UpdateValue();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltipLine = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Name == "Material");
            tooltips.Remove(tooltipLine);

            // 遗器等级
            if (!isDisplay)
            {
                TooltipLine line = new(Mod, "Level", $"+{level}")
                {
                    OverrideColor = new Color(255, 255, 0)
                };
                tooltips.Add(line);
            }

            // 主词条信息
            if (MainEntry != null && !isDisplay)
            {
                TooltipLine line1 = new(Mod, "MainEntry", $"{MainEntryText[(int)MainEntry].WithFormatArgs(Math.Round(MainEntryBonus[(int)MainEntry] * level, 1).ToString("F1")).Value}")
                {
                    OverrideColor = new Color(0, 255, 255)
                };
                tooltips.Add(line1);
            }

            // 副词条信息
            if (!isDisplay)
            {
                for (int i = 0; i < AdverbEntryList.Count; i++)
                {
                    TooltipLine line2 = new(Mod, "AdverbEntryList", $"{AdverbEntryText[(int)AdverbEntryList[i]].WithFormatArgs(Math.Round(AdverbEntryBonus[(int)AdverbEntryList[i]] * AdverbEntryNumList[i], 1).ToString("F1")).Value}")
                    {
                        OverrideColor = new Color(0, 255, 0)
                    };
                    tooltips.Add(line2);
                }
            }

            // 套装效果
            TooltipLine line3;
            if (isTwoSet || isDisplay)
            {
                line3 = new(Mod, "RelicSet", $"{GetTextValue("Mods.StarRailRelic.Items.SetTwo")}\n{GetTextValue($"Mods.StarRailRelic.Items.{RelicSet}SetTwo")}")
                {
                    OverrideColor = new Color(255, 110, 180)
                };
            }
            else
            {
                line3 = new(Mod, "RelicSet", $"{GetTextValue("Mods.StarRailRelic.Items.SetTwo")}{GetTextValue("Mods.StarRailRelic.Items.NoSet")}\n{GetTextValue($"Mods.StarRailRelic.Items.{RelicSet}SetTwo")}")
                {
                    OverrideColor = new Color(79, 79, 79)
                };
            }
            tooltips.Add(line3);

            TooltipLine line4;
            if (!Exists($"Mods.StarRailRelic.Items.{RelicSet}SetFour"))
            {
                return;
            }

            if (isFourSet || isDisplay)
            {
                line4 = new(Mod, "RelicSet", $"{GetTextValue("Mods.StarRailRelic.Items.SetFour")}\n{GetTextValue($"Mods.StarRailRelic.Items.{RelicSet}SetFour")}")
                {
                    OverrideColor = new Color(255, 110, 180)
                };
            }
            else
            {
                line4 = new(Mod, "RelicSet", $"{GetTextValue("Mods.StarRailRelic.Items.SetFour")}{GetTextValue("Mods.StarRailRelic.Items.NoSet")}\n{GetTextValue($"Mods.StarRailRelic.Items.{RelicSet}SetFour")}")
                {
                    OverrideColor = new Color(79, 79, 79)
                };
            }
            tooltips.Add(line4);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 更新遗器的价值，计算基于主词条和副词条的综合价值.
        /// </summary>
        /// <remarks>
        /// 此方法会根据当前遗器的等级和副词条的出现次数来更新遗器的销售价格.
        /// <para>计算逻辑如下：</para>
        /// <list type="bullet">
        /// <item>
        /// <description><c>adverbValue</c>: 计算与所有副词条的出现次数相关的价值，</description>
        /// <description> 通过将副词条的总出现次数乘以每个副词条的基础价格（50银币）来得出.</description>
        /// </item>
        /// <item>
        /// <description><c>mainValue</c>: 计算与遗器等级相关的价值，</description>
        /// <description> 是当前等级乘以每级的基础价格（50银币）.</description>
        /// </item>
        /// <item>
        /// <description><c>baseValue</c>: 赋予遗器一个基础价值1金币.</description>
        /// </item>
        /// <item>
        /// <description>最后，将 <c>baseValue</c>、<c>mainValue</c> 和 <c>adverbValue</c> 相加，并乘以 0.66，</description>
        /// <description>用于调整最终价值 <c>Item.value</c>。</description>
        /// </item>
        /// </list>
        /// </remarks>
        public void UpdateValue()
        {
            int adverbValue = AdverbEntryNumList.Sum() * Item.sellPrice(0, 0, 50, 0);
            int mainValue = level * Item.sellPrice(0, 0, 50, 0);
            int baseValue = Item.sellPrice(0, 1, 0, 0);
            Item.value = (int)((baseValue + mainValue + adverbValue) * 0.66);
        }

        /// <summary>
        /// 升级遗器并在达到特定等级时增加或升级副词条.
        /// </summary>
        /// <remarks>
        /// 每次调用此方法，当前遗器的等级将提升 1，最高可升级到 <see cref="levelMax"/>.
        /// <para>具体规则如下：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>当等级达到 3 级时，如果副词条数量为 3，并且待定副词条不为空，</description>
        /// <description>则将待定副词条添加到副词条列表中，并将其数量设置为 1。</description>
        /// </item>
        /// <item>
        /// <description>每当等级达到 3 的倍数时，将随机增加一个已存在副词条的出现次数。</description>
        /// </item>
        /// </list>
        /// </remarks>
        public void LevelUp()
        {
            if (level < levelMax)
            {
                level++;
                if (level == 3 && AdverbEntryList.Count == 3 && pendingAdverbEntry.HasValue)
                {
                    AdverbEntryList.Add(pendingAdverbEntry.Value);
                    AdverbEntryNumList.Add(1);
                    pendingAdverbEntry = null;
                }
                else if (level % 3 == 0)
                {
                    AdverbEntryNumList[Main.rand.Next(AdverbEntryNumList.Count)]++;
                }
            }

            UpdateValue();
        }

        /// <summary>
        /// 激活遗器的二件套效果并应用相应的特殊效果.
        /// </summary>
        /// <param name="player">目标玩家对象，需要获取该玩家的套装效果。</param>
        /// <remarks>
        /// 该方法将遗器的二件套状态设置为激活，并调用相应的方法来应用该效果.
        /// </remarks>
        public void SetToTwoSet(Player player)
        {
            isTwoSet = true;
            ModifyTwoSetSpecialEffect(player.GetModPlayer<RelicSetSpecialEffectPlayer>());
        }

        /// <summary>
        /// 激活遗器的四件套效果并应用相应的特殊效果.
        /// </summary>
        /// <param name="player">目标玩家对象，需要获取该玩家的套装效果。</param>
        /// <remarks>
        /// 该方法将遗器的四件套状态设置为激活，并调用相应的方法来应用该效果.
        /// </remarks>
        public void SetToFourSet(Player player)
        {
            isFourSet = true;
            ModifyFourSetSpecialEffect(player?.GetModPlayer<RelicSetSpecialEffectPlayer>());
        }

        /// <summary>
        /// 清除遗器的套装效果，将其状态设置为无套装效果.
        /// </summary>
        /// <remarks>
        /// 调用此方法后，遗器将不再应用任何套装效果.
        /// </remarks>
        public void SetToNoSet()
        {
            isTwoSet = false;
            isFourSet = false;
        }
        #endregion

        #region 虚方法
        /// <summary>
        /// 更新遗器的二件套效果。
        /// <para>子类可以重写此方法，以实现特定的遗器二件套效果。</para>
        /// </summary>
        /// <param name="player">目标玩家实例，应用二件套效果的对象。</param>
        public virtual void UpdateRelicSetTwo(Player player) { }

        /// <summary>
        /// 更新遗器的四件套效果。
        /// <para>子类可以重写此方法，以实现特定的遗器四件套效果。</para>
        /// </summary>
        /// <param name="player">目标玩家实例，应用四件套效果的对象。</param>
        public virtual void UpdateRelicSetFour(Player player){ }

        /// <summary>
        /// 修改与二件套效果相关的特殊效果.
        /// <para>子类可以重写此方法，以实现特定的二件套效果逻辑。</para>
        /// </summary>
        /// <param name="modPlayer">包含与遗器二件套效果相关的特殊效果的ModPlayer实例。</param>
        public virtual void ModifyTwoSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer) { }

        /// <summary>
        /// 修改与四件套效果相关的特殊效果.
        /// <para>子类可以重写此方法，以实现特定的四件套效果逻辑。</para>
        /// </summary>
        /// <param name="modPlayer">包含与遗器四件套效果相关的特殊效果的ModPlayer实例。</param>
        public virtual void ModifyFourSetSpecialEffect(RelicSetSpecialEffectPlayer modPlayer) { }
        #endregion

        #region 内部方法
        /// <summary>
        /// 根据遗器类型设置主词条池，并从中随机抽取主词条.
        /// </summary>
        /// <remarks>
        /// 此方法会根据遗器的类型（如头部、手部、躯干、脚部等）来分配适当的主词条池.
        /// <para>具体规则如下：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.Head"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.LifeFlat"/>：最终生命值提高。</description>
        /// </item>
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.Hands"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.DamageFlat"/>：最终伤害提高。</description>
        /// </item>
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.Body"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.CritChance"/>：暴击率提高。</description>
        /// <description><see cref="RelicMainEntryType.LifeRegen"/>：生命再生速率提高。</description>
        /// <description><see cref="RelicMainEntryType.MeleeAttackSpeed"/>：近战攻击速度提高。</description>
        /// <description><see cref="RelicMainEntryType.LifeAdditive"/>：生命值百分比提高。</description>
        /// <description><see cref="RelicMainEntryType.DamageAdditive"/>：伤害百分比提高。</description>
        /// </item>
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.Feet"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.Defense"/>：防御力提高。</description>
        /// <description><see cref="RelicMainEntryType.Endurance"/>：免伤提高。</description>
        /// <description><see cref="RelicMainEntryType.LifeAdditive"/>：生命值百分比提高。</description>
        /// <description><see cref="RelicMainEntryType.DamageAdditive"/>：伤害百分比提高。</description>
        /// </item>
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.PlanarSphere"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.MeleeArmorPenetration"/>：近战伤害盔甲穿透提高。</description>
        /// <description><see cref="RelicMainEntryType.RangedArmorPenetration"/>：远程伤害盔甲穿透提高。</description>
        /// <description><see cref="RelicMainEntryType.MagicArmorPenetration"/>：魔法伤害盔甲穿透提高。</description>
        /// <description><see cref="RelicMainEntryType.SummonArmorPenetration"/>：召唤伤害盔甲穿透提高。</description>
        /// <description><see cref="RelicMainEntryType.LifeAdditive"/>：生命值百分比提高。</description>
        /// <description><see cref="RelicMainEntryType.DamageAdditive"/>：伤害百分比提高。</description>
        /// </item>
        /// <item>
        /// <description>如果遗器类型为 <see cref="RelicType.LinkRope"/>，则主词条池包含：</description>
        /// <description><see cref="RelicMainEntryType.ManaCostReduction"/>：魔力消耗降低。</description>
        /// <description><see cref="RelicMainEntryType.MaxMinions"/>：召唤物上限提高。</description>
        /// <description><see cref="RelicMainEntryType.LifeAdditive"/>：生命值百分比提高。</description>
        /// <description><see cref="RelicMainEntryType.DamageAdditive"/>：伤害百分比提高。</description>
        /// </item>
        /// </list>
        /// <para>然后，它会从这个主词条池中随机选择一个主词条，并将其设置为当前遗器的主词条（<see cref="MainEntry"/>）。</para>
        /// </remarks>
        private void SetMainEntryByRelicType()
        {
            // 分配主词条池
            switch (RelicType)
            {
                case RelicType.Head:
                    mainEntryPool = [RelicMainEntryType.LifeFlat];
                    break;
                case RelicType.Hands:
                    mainEntryPool = [RelicMainEntryType.DamageFlat];
                    break;
                case RelicType.Body:
                    mainEntryPool =
                    [
                        RelicMainEntryType.CritChance,
                        RelicMainEntryType.LifeRegen,
                        RelicMainEntryType.MeleeAttackSpeed,
                        RelicMainEntryType.LifeAdditive,
                        RelicMainEntryType.DamageAdditive
                    ];
                    break;
                case RelicType.Feet:
                    mainEntryPool =
                    [
                        RelicMainEntryType.Defense,
                        RelicMainEntryType.Endurance,
                        RelicMainEntryType.LifeAdditive,
                        RelicMainEntryType.DamageAdditive
                    ];
                    break;
                case RelicType.PlanarSphere:
                    mainEntryPool =
                    [
                        RelicMainEntryType.MeleeArmorPenetration,
                        RelicMainEntryType.RangedArmorPenetration,
                        RelicMainEntryType.MagicArmorPenetration,
                        RelicMainEntryType.SummonArmorPenetration,
                        RelicMainEntryType.LifeAdditive,
                        RelicMainEntryType.DamageAdditive
                    ];
                    break;
                case RelicType.LinkRope:
                    mainEntryPool =
                    [
                        RelicMainEntryType.ManaCostReduction,
                        RelicMainEntryType.MaxMinions,
                        RelicMainEntryType.LifeAdditive,
                        RelicMainEntryType.DamageAdditive
                    ];
                    break;
            }

            // 抽取主词条
            MainEntry ??= mainEntryPool[Main.rand.Next(mainEntryPool.Count)];
        }

        /// <summary>
        /// 初始化副词条
        /// </summary>
        private void InitializeAdverbEntries()
        {
            AdverbEntryList = [];
            AdverbEntryNumList = [];
            int entryCount = Main.rand.NextDouble() < 0.25 ? 4 : 3;

            List<RelicAdverbEntryType> allAdverbEntries = Enum.GetValues(typeof(RelicAdverbEntryType)).Cast<RelicAdverbEntryType>().ToList();
            HashSet<RelicAdverbEntryType> selectedEntries = [];
            while (AdverbEntryList.Count < entryCount && selectedEntries.Count < allAdverbEntries.Count)
            {
                RelicAdverbEntryType randomAdverb = allAdverbEntries[Main.rand.Next(allAdverbEntries.Count)];

                if (selectedEntries.Add(randomAdverb))
                {
                    AdverbEntryList.Add(randomAdverb);
                    AdverbEntryNumList.Add(1);
                }
            }

            if (entryCount == 3)
            {
                while (pendingAdverbEntry == null)
                {
                    RelicAdverbEntryType randomAdverb = allAdverbEntries[Main.rand.Next(allAdverbEntries.Count)];

                    if (selectedEntries.Add(randomAdverb))
                    {
                        pendingAdverbEntry = randomAdverb;
                    }
                }
            }
        }

        private static Dictionary<string, int> CreateRelicList(string namespacePrefix, string version, bool isOutRelic = true)
        {
            string[] relicTypes;

            if (isOutRelic)
            {
                relicTypes =
                [
                    $"StarRailRelic.Content.Items.Relic.Out.{version}.{namespacePrefix}.{namespacePrefix}Head",
                    $"StarRailRelic.Content.Items.Relic.Out.{version}.{namespacePrefix}.{namespacePrefix}Hands",
                    $"StarRailRelic.Content.Items.Relic.Out.{version}.{namespacePrefix}.{namespacePrefix}Body",
                    $"StarRailRelic.Content.Items.Relic.Out.{version}.{namespacePrefix}.{namespacePrefix}Feet"
                ];
            }
            else
            {
                relicTypes =
                [
                    $"StarRailRelic.Content.Items.Relic.In.{version}.{namespacePrefix}.{namespacePrefix}Sphere",
                    $"StarRailRelic.Content.Items.Relic.In.{version}.{namespacePrefix}.{namespacePrefix}Rope"
                ];
            }

            Dictionary<string, int> itemTypes = [];

            foreach (string typeName in relicTypes)
            {
                // 使用反射创建实例并获取 Item.type
                Type type = System.Type.GetType(typeName);
                if (type != null && typeof(ModItem).IsAssignableFrom(type))
                {
                    object instance = typeof(ContentInstance<>).MakeGenericType(type).GetProperty("Instance").GetValue(null);
                    if (instance is ModItem modItem)
                    {
                        itemTypes[modItem.Name] = modItem.Type;
                    }
                }
            }

            return itemTypes;
        }

        #endregion

        #region 保存和加载
        public override void SaveData(TagCompound tag)
        {
            tag["level"] = level;

            if (MainEntry != null)
            {
                tag["MainEntry"] = MainEntry.ToString();
            }

            tag["AdverbEntryList"] = AdverbEntryList.Select(entry => (int)entry).ToList();
            tag["AdverbEntryNumList"] = AdverbEntryNumList;

            if (pendingAdverbEntry != null)
            {
                tag["pendingAdverbEntry"] = pendingAdverbEntry.ToString();
            }
        }

        public override void LoadData(TagCompound tag)
        {
            level = tag.GetInt("level");

            if (tag.ContainsKey("MainEntry"))
            {
                string mainEntryStr = tag.GetString("MainEntry");

                if (string.IsNullOrEmpty(mainEntryStr))
                {
                    throw new ArgumentException("MainEntry string value is empty or null.");
                }

                try
                {
                    MainEntry = (RelicMainEntryType)Enum.Parse(typeof(RelicMainEntryType), mainEntryStr);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Invalid value for RelicMainEntryType: {mainEntryStr}", ex);
                }
            }

            AdverbEntryList = tag.Get<List<int>>("AdverbEntryList").Select(i => (RelicAdverbEntryType)i).ToList();
            AdverbEntryNumList = tag.Get<List<int>>("AdverbEntryNumList");

            if (tag.ContainsKey("pendingAdverbEntry"))
            {
                string pendingAdverbEntryStr = tag.GetString("pendingAdverbEntry");

                if (string.IsNullOrEmpty(pendingAdverbEntryStr))
                {
                    throw new ArgumentException("pendingAdverbEntry string value is empty or null.");
                }

                try
                {
                    pendingAdverbEntry = (RelicAdverbEntryType)Enum.Parse(typeof(RelicAdverbEntryType), pendingAdverbEntryStr);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Invalid value for RelicAdverbEntryType: {pendingAdverbEntryStr}", ex);
                }
            }
        }
        #endregion
    }
}
