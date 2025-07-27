namespace StarRailRelic.Common.Players
{
    /// <summary>
    /// 表示玩家的遗器数据和效果的管理类.
    /// 该类继承自 <see cref="ModPlayer"/>，并负责存储玩家装备的遗器信息，以及更新和应用遗器的效果。
    /// </summary>
    /// <remarks>
    /// 此类通过维护六个不同类型的遗器（头部、手部、躯干、脚部、位面球和连结绳）来实现遗器系统的功能。
    /// <para>主要功能包括：</para>
    /// <list type="bullet">
    /// <item><description>重置遗器效果，确保在每次游戏逻辑更新中正确处理遗器状态。</description></item>
    /// <item><description>更新遗器提供的伤害、免伤、暴击、攻速、穿透、魔力减耗、召唤物上限和防御等效果。</description></item>
    /// <item><description>处理遗器套装效果的逻辑，根据装备的遗器数量触发不同的套装效果。</description></item>
    /// <item><description>保存与加载玩家的遗器数据。</description></item>
    /// </list>
    /// </remarks>
    public class RelicPlayer : ModPlayer
    {
        public Item HeadRelic;
        public Item HandsRelic;
        public Item BodyRelic;
        public Item FeetRelic;
        public Item PlanarSphereRelic;
        public Item LinkRopeRelic;

        #region 更新效果
        public override void ResetEffects()
        {
            foreach (Item item in new[] { HeadRelic, HandsRelic, BodyRelic, FeetRelic, PlanarSphereRelic, LinkRopeRelic })
            {
                if (item.IsValidRelic(out ModRelic relic))
                {
                    relic.SetToNoSet();
                }
            }
        }

        // 更新常规效果：伤害，免伤，暴击，攻速，穿透，魔力减耗，召唤栏，防御
        public override void UpdateEquips()
        {
            foreach (var field in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.GetValue(this) is Item relicItem)
                {
                    UpdateRelicEffect(relicItem);
                }
            }

            UpdateRelicSet();
        }

        #region 更新遗器套装效果
        /// <summary>
        /// 更新遗器套装效果
        /// </summary>
        private void UpdateRelicSet()
        {
            // 统计相同类型的遗器数量
            Dictionary<RelicSet, int> relicCounts = [];

            CheckRelicCount(HeadRelic, relicCounts);
            CheckRelicCount(HandsRelic, relicCounts);
            CheckRelicCount(BodyRelic, relicCounts);
            CheckRelicCount(FeetRelic, relicCounts);
            CheckRelicCount(PlanarSphereRelic, relicCounts);
            CheckRelicCount(LinkRopeRelic, relicCounts);

            // 检查套装效果
            foreach (var pair in relicCounts)
            {
                if (pair.Value >= 4) // 四个或以上相同类型遗器
                {
                    TriggerSetFourEffect(pair.Key);
                }
                if (pair.Value >= 2) // 两个或以上相同类型遗器
                {
                    TriggerSetTwoEffect(pair.Key);
                }
            }
        }

        /// <summary>
        /// 检查遗器数量
        /// </summary>
        private static void CheckRelicCount(Item item, Dictionary<RelicSet, int> relicCounts)
        {
            if (item.IsValidRelic(out ModRelic relic))
            {
                RelicSet set = relic.RelicSet;

                if (!relicCounts.TryGetValue(set, out int value))
                {
                    value = 0;
                    relicCounts[set] = value;
                }

                relicCounts[set] = ++value;
            }
        }

        private void TriggerSetTwoEffect(RelicSet relicSet)
        {
            // 根据遗器套装类型触发相应的方法
            foreach (Item item in new[] { HeadRelic, HandsRelic, BodyRelic, FeetRelic, PlanarSphereRelic, LinkRopeRelic })
            {
                if (item.IsValidRelic(out ModRelic relic) && relic.RelicSet == relicSet)
                {
                    relic.SetToTwoSet(Player);
                }
            }

            foreach (Item item in new[] { HeadRelic, HandsRelic, BodyRelic, FeetRelic, PlanarSphereRelic, LinkRopeRelic })
            {
                if (item.IsValidRelic(out ModRelic relic) && relic.RelicSet == relicSet)
                {
                    relic.UpdateRelicSetTwo(Player);

                    return;// 找到并触发后立即返回，避免多次触发
                }
            }
        }

        private void TriggerSetFourEffect(RelicSet relicSet)
        {
            // 根据遗器套装类型触发相应的方法
            foreach (Item item in new[] { HeadRelic, HandsRelic, BodyRelic, FeetRelic, PlanarSphereRelic, LinkRopeRelic })
            {
                if (item.IsValidRelic(out ModRelic relic) && relic.RelicSet == relicSet)
                {
                    relic.SetToFourSet(Player);
                }
            }

            foreach (Item item in new[] { HeadRelic, HandsRelic, BodyRelic, FeetRelic, PlanarSphereRelic, LinkRopeRelic })
            {
                if (item.IsValidRelic(out ModRelic relic) && relic.RelicSet == relicSet)
                {
                    relic.UpdateRelicSetFour(Player);

                    return;// 找到并触发后立即返回，避免多次触发
                }
            }
        }
        #endregion

        #region 更新常规效果
        private void UpdateRelicEffect(Item relicItem)
        {
            if (relicItem?.ModItem is ModRelic relic && relic.MainEntry != null)
            {
                UpdateMainEffect(relic);

                UpdateAdverbEffect(relic);
            }
        }

        private void UpdateMainEffect(ModRelic relic)
        {
            float entryBonus = ModRelic.MainEntryBonus[(int)relic.MainEntry] * relic.level;

            switch (relic.MainEntry)
            {
                case RelicMainEntryType.DamageFlat:
                    Player.GetDamage<GenericDamageClass>().Flat += entryBonus;
                    break;
                case RelicMainEntryType.DamageAdditive:
                    Player.GetDamage<GenericDamageClass>() += entryBonus / 100f;
                    break;
                case RelicMainEntryType.Endurance:
                    Player.endurance += entryBonus / 100f;
                    break;
                case RelicMainEntryType.CritChance:
                    Player.GetCritChance<GenericDamageClass>() += entryBonus;
                    break;
                case RelicMainEntryType.MeleeAttackSpeed:
                    Player.GetAttackSpeed<MeleeDamageClass>() += entryBonus / 100f;
                    Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += entryBonus / 100f;
                    break;
                case RelicMainEntryType.MeleeArmorPenetration:
                    Player.GetArmorPenetration<MeleeDamageClass>() += entryBonus;
                    break;
                case RelicMainEntryType.RangedArmorPenetration:
                    Player.GetArmorPenetration<RangedDamageClass>() += entryBonus;
                    break;
                case RelicMainEntryType.MagicArmorPenetration:
                    Player.GetArmorPenetration<MagicDamageClass>() += entryBonus;
                    break;
                case RelicMainEntryType.SummonArmorPenetration:
                    Player.GetArmorPenetration<SummonDamageClass>() += entryBonus;
                    break;
                case RelicMainEntryType.ManaCostReduction:
                    Player.manaCost -= entryBonus / 100f;
                    break;
                case RelicMainEntryType.MaxMinions:
                    Player.maxMinions += (int)entryBonus;
                    break;
                case RelicMainEntryType.Defense:
                    Player.statDefense += (int)entryBonus;
                    break;
            }
        }

        private void UpdateAdverbEffect(ModRelic relic)
        {
            for (int i = 0; i < relic.AdverbEntryList.Count; i++)
            {
                RelicAdverbEntryType adverbEntry = relic.AdverbEntryList[i];
                float adverbBonus = ModRelic.AdverbEntryBonus[(int)adverbEntry] * relic.AdverbEntryNumList[i];

                switch (adverbEntry)
                {
                    case RelicAdverbEntryType.DamageAdditive:
                        Player.GetDamage<GenericDamageClass>() += adverbBonus / 100f;
                        break;
                    case RelicAdverbEntryType.CritChance:
                        Player.GetCritChance<GenericDamageClass>() += adverbBonus;
                        break;
                    case RelicAdverbEntryType.MeleeAttackSpeed:
                        Player.GetAttackSpeed<MeleeDamageClass>() += adverbBonus / 100f;
                        Player.GetAttackSpeed<SummonMeleeSpeedDamageClass>() += adverbBonus / 100f;
                        break;
                    case RelicAdverbEntryType.ArmorPenetration:
                        Player.GetArmorPenetration<GenericDamageClass>() += adverbBonus;
                        break;
                    case RelicAdverbEntryType.ManaCostReduction:
                        Player.manaCost -= adverbBonus / 100f;
                        break;
                    case RelicAdverbEntryType.Defense:
                        Player.statDefense += (int)adverbBonus;
                        break;
                }
            }
        }
        #endregion 

        // 更新生命回复效果
        public override void UpdateLifeRegen()
        {
            foreach (var field in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.GetValue(this) is Item relicItem)
                {
                    UpdateRelicLifeRegen(relicItem);
                }
            }
        }

        #region 更新生命回复效果
        private void UpdateRelicLifeRegen(Item relicItem)
        {
            if (relicItem?.ModItem is ModRelic relic && relic.MainEntry != null)
            {
                UpdateMainLifeRegen(relic);

                UpdateAdverbLifeRegen(relic);
            }
        }

        private void UpdateMainLifeRegen(ModRelic relic)
        {
            if (relic.MainEntry == RelicMainEntryType.LifeRegen)
            {
                Player.lifeRegen += (int)(ModRelic.MainEntryBonus[(int)relic.MainEntry] * relic.level * 2);
            }
        }

        private void UpdateAdverbLifeRegen(ModRelic relic)
        {
            for (int i = 0; i < relic.AdverbEntryList.Count; i++)
            {
                RelicAdverbEntryType adverbEntry = relic.AdverbEntryList[i];
                float adverbBonus = ModRelic.AdverbEntryBonus[(int)adverbEntry] * relic.AdverbEntryNumList[i];

                if (adverbEntry == RelicAdverbEntryType.LifeRegen)
                {
                    Player.lifeRegen += (int)(adverbBonus * 2);
                }
            }
        }
        #endregion

        // 更新最大生命与法力效果
        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            health = StatModifier.Default;

            foreach (var field in GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.GetValue(this) is Item relicItem)
                {
                    UpdateRelicMaxStats(relicItem, ref health);
                }
            }

            mana = StatModifier.Default;
        }

        #region 更新最大生命与法力效果
        private static void UpdateRelicMaxStats(Item relicItem, ref StatModifier health)
        {
            if (relicItem?.ModItem is ModRelic relic && relic.MainEntry != null)
            {
                UpdateMainMaxStats(relic, ref health);

                UpdateAdverbMaxStats(relic, ref health);
            }
        }

        private static void UpdateMainMaxStats(ModRelic relic, ref StatModifier health)
        {
            float entryBonus = ModRelic.MainEntryBonus[(int)relic.MainEntry] * relic.level;

            switch (relic.MainEntry)
            {
                case RelicMainEntryType.LifeFlat:
                    health.Flat += entryBonus;
                    break;
                case RelicMainEntryType.LifeAdditive:
                    health *= (1 + entryBonus / 100f);
                    break;
            }
        }

        private static void UpdateAdverbMaxStats(ModRelic relic, ref StatModifier health)
        {
            for (int i = 0; i < relic.AdverbEntryList.Count; i++)
            {
                RelicAdverbEntryType adverbEntry = relic.AdverbEntryList[i];
                float adverbBonus = ModRelic.AdverbEntryBonus[(int)adverbEntry] * relic.AdverbEntryNumList[i];

                switch (adverbEntry)
                {
                    case RelicAdverbEntryType.LifeFlat:
                        health.Flat += adverbBonus;
                        break;
                    case RelicAdverbEntryType.LifeAdditive:
                        health *= (1 + adverbBonus / 100f);
                        break;
                }
            }
        }
        #endregion
        #endregion

        #region 网络同步
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)StarRailRelic.MessageType.RelicPlayer);
            packet.Write((byte)Player.whoAmI);

            SyncRelicSlot(packet, HeadRelic);
            SyncRelicSlot(packet, HandsRelic);
            SyncRelicSlot(packet, BodyRelic);
            SyncRelicSlot(packet, FeetRelic);
            SyncRelicSlot(packet, PlanarSphereRelic);
            SyncRelicSlot(packet, LinkRopeRelic);

            packet.Send(toWho, fromWho);
        }

        // 同步单个遗器槽位
        private static void SyncRelicSlot(ModPacket packet, Item relicItem)
        {
            // 是否有遗器
            bool hasRelic = relicItem != null && relicItem.ModItem is ModRelic;
            packet.Write(hasRelic);

            if (!hasRelic)
            {
                return;
            }

            ModRelic modRelic = relicItem.ModItem as ModRelic;

            // 写入遗器类型
            packet.Write(relicItem.type);

            // 写入等级
            packet.Write((byte)modRelic.level);

            // 写入主词条（使用byte表示枚举值）
            packet.Write(modRelic.MainEntry.HasValue);
            if (modRelic.MainEntry.HasValue)
            {
                packet.Write((byte)modRelic.MainEntry.Value);
            }

            // 写入副词条数量
            int adverbCount = modRelic.AdverbEntryList?.Count ?? 0;
            packet.Write((byte)adverbCount);

            // 写入每个副词条
            for (int i = 0; i < adverbCount; i++)
            {
                packet.Write((byte)modRelic.AdverbEntryList[i]);
                packet.Write((byte)modRelic.AdverbEntryNumList[i]);
            }
        }

        public void ReceivePlayerSync(BinaryReader reader)
        {
            HeadRelic = ReadRelicSlot(reader);
            HandsRelic = ReadRelicSlot(reader);
            BodyRelic = ReadRelicSlot(reader);
            FeetRelic = ReadRelicSlot(reader);
            PlanarSphereRelic = ReadRelicSlot(reader);
            LinkRopeRelic = ReadRelicSlot(reader);
        }

        // 读取单个遗器槽位
        private static Item ReadRelicSlot(BinaryReader reader)
        {
            // 检查是否有遗器
            bool hasRelic = reader.ReadBoolean();
            if (!hasRelic)
            {
                return new Item();
            }

            // 创建物品实例
            int itemType = reader.ReadInt32();
            Item relicItem = new();
            relicItem.SetDefaults(itemType);

            // 获取ModRelic组件
            ModRelic modRelic = relicItem.ModItem as ModRelic;

            // 读取等级
            modRelic.level = reader.ReadByte();

            // 读取主词条
            bool hasMainEntry = reader.ReadBoolean();
            if (hasMainEntry)
            {
                modRelic.MainEntry = (RelicMainEntryType)reader.ReadByte();
            }

            // 读取副词条数量
            int adverbCount = reader.ReadByte();
            modRelic.AdverbEntryList = new List<RelicAdverbEntryType>(adverbCount);
            modRelic.AdverbEntryNumList = new List<int>(adverbCount);

            // 读取每个副词条
            for (int i = 0; i < adverbCount; i++)
            {
                modRelic.AdverbEntryList.Add((RelicAdverbEntryType)reader.ReadByte());
                modRelic.AdverbEntryNumList.Add(reader.ReadByte());
            }

            return relicItem;
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            RelicPlayer clone = (RelicPlayer)targetCopy;
            clone.HeadRelic = HeadRelic;
            clone.HandsRelic = HandsRelic;
            clone.BodyRelic = BodyRelic;
            clone.FeetRelic = FeetRelic;
            clone.PlanarSphereRelic = PlanarSphereRelic;
            clone.LinkRopeRelic = LinkRopeRelic;
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            RelicPlayer clientClone = (RelicPlayer)clientPlayer;

            // 检查遗器是否发生变化
            bool relicsChanged = !RelicEquals(HeadRelic, clientClone.HeadRelic) ||
                                 !RelicEquals(HandsRelic, clientClone.HandsRelic) ||
                                 !RelicEquals(BodyRelic, clientClone.BodyRelic) ||
                                 !RelicEquals(FeetRelic, clientClone.FeetRelic) ||
                                 !RelicEquals(PlanarSphereRelic, clientClone.PlanarSphereRelic) ||
                                 !RelicEquals(LinkRopeRelic, clientClone.LinkRopeRelic);

            // 如果有变化，触发同步
            if (relicsChanged)
            {
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
            }
        }

        // 比较两个遗器是否相同
        private static bool RelicEquals(Item a, Item b)
        {
            if (a.type != b.type)
            {
                return false;
            }

            return a.ModItem is ModRelic relicA && 
                   b.ModItem is ModRelic relicB && 
                   relicA.MainEntry == relicB.MainEntry &&
                   relicA.level == relicB.level &&
                   relicA.AdverbEntryList.SequenceEqual(relicB.AdverbEntryList) &&
                   relicA.AdverbEntryNumList.SequenceEqual(relicB.AdverbEntryNumList);
        }
        #endregion

        #region 保存数据
        public override void SaveData(TagCompound tag)
        {
            tag["HeadRelic"] = HeadRelic;
            tag["HandsRelic"] = HandsRelic;
            tag["BodyRelic"] = BodyRelic;
            tag["FeetRelic"] = FeetRelic;
            tag["PlanarSphereRelic"] = PlanarSphereRelic;
            tag["LinkRopeRelic"] = LinkRopeRelic;
        }

        public override void LoadData(TagCompound tag)
        {
            HeadRelic = tag.Get<Item>("HeadRelic");
            HandsRelic = tag.Get<Item>("HandsRelic");
            BodyRelic = tag.Get<Item>("BodyRelic");
            FeetRelic = tag.Get<Item>("FeetRelic");
            PlanarSphereRelic = tag.Get<Item>("PlanarSphereRelic");
            LinkRopeRelic = tag.Get<Item>("LinkRopeRelic");
        }
        #endregion
    }
}
