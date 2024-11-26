namespace StarRailRelic
{
    /// <summary>
    /// 主mod类，负责初始化和管理与遗器相关的功能和逻辑。
    /// </summary>
    /// <remarks>
    /// 此类通过 Hooking 的方式拦截并修改游戏中的物品槽的右键点击事件，
    /// 旨在扩展遗器的交互功能。具体包括遗器的选取、交换与放置。
    /// <para>主要功能包括：</para>
    /// <list type="bullet">
    /// <item><description>加载右键点击事件的处理逻辑。</description></item>
    /// <item><description>根据游戏的上下文来执行物品的操作，包括遗器的交互。</description></item>
    /// <item><description>在实现独特的遗器交换逻辑时处理悬浮和点击事件。</description></item>
    /// <item><description>通过卸载清理已挂钩的事件，确保不会引发内存泄漏。</description></item>
    /// </list>
    /// </remarks>
    public class StarRailRelic : Mod
	{
        public override void Load()
        {
            On_ItemSlot.RightClick_ItemArray_int_int += On_ItemSlot_RightClick_ItemArray_int_int;
            On_Player.HealEffect += On_Player_HealEffect;
            On_Player.ManaEffect += On_Player_ManaEffect;
        }

        private void On_ItemSlot_RightClick_ItemArray_int_int(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            orig.Invoke(inv, context, slot);
            SwapRelic(inv, context, slot);
        }

        private void On_Player_HealEffect(On_Player.orig_HealEffect orig, Player self, int healAmount, bool broadcast)
        {
            orig.Invoke(self, healAmount, broadcast);

            if (healAmount > 50)
            {
                self.GetModPlayer<RelicSetSpecialEffectPlayer>().OnHealLife();
            }
        }

        private void On_Player_ManaEffect(On_Player.orig_ManaEffect orig, Player self, int manaAmount)
        {
            orig.Invoke(self, manaAmount);

            if (manaAmount > 50)
            {
                self.GetModPlayer<RelicSetSpecialEffectPlayer>().OnHealMana();
            }
        }

        private static void SwapRelic(Item[] inv, int context, int slot)
        {
            if (Main.mouseRight && context == 0 && inv[slot].IsValidRelic(out ModRelic relic) && Main.mouseRightRelease)
            {
                RelicMainUI relicMainUI = GetInstance<RelicMainUISystem>().uiState;
                List<RelicUnitSlot> relicUnitSlots =
                [
                        relicMainUI.slot1,
                        relicMainUI.slot2,
                        relicMainUI.slot3,
                        relicMainUI.slot4,
                        relicMainUI.slot5,
                        relicMainUI.slot6
                ];

                Item clickItem = inv[slot];
                Item relicItem = relicUnitSlots[(int)relic.RelicType].relicItem;

                if (relicItem.IsValidRelic(out ModRelic relic1))// 若右键时点击遗器和栏位遗器均不为空，交换鼠标遗器和栏位遗器
                {
                    relic1.SetToNoSet();
                    relic1.UpdateValue();
                    Item lastRelicItem = relicItem.Clone();

                    relicUnitSlots[(int)relic.RelicType].relicItem = clickItem.Clone();
                    inv[slot] = lastRelicItem;
                }
                else// 若右键时点击遗器不为空，栏位遗器为空，放置鼠标遗器
                {
                    relicUnitSlots[(int)relic.RelicType].relicItem = clickItem.Clone();

                    clickItem.SetDefaults();
                }

                relic.UpdateValue();
                SoundEngine.PlaySound(SoundID.Grab);
            }
        }

        public override void Unload()
        {
            On_ItemSlot.RightClick_ItemArray_int_int -= On_ItemSlot_RightClick_ItemArray_int_int;
            On_Player.HealEffect -= On_Player_HealEffect;
            On_Player.ManaEffect -= On_Player_ManaEffect;
        }
    }
}
