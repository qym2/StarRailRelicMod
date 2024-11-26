namespace StarRailRelic
{
    /// <summary>
    /// ��mod�࣬�����ʼ���͹�����������صĹ��ܺ��߼���
    /// </summary>
    /// <remarks>
    /// ����ͨ�� Hooking �ķ�ʽ���ز��޸���Ϸ�е���Ʒ�۵��Ҽ�����¼���
    /// ּ����չ�����Ľ������ܡ��������������ѡȡ����������á�
    /// <para>��Ҫ���ܰ�����</para>
    /// <list type="bullet">
    /// <item><description>�����Ҽ�����¼��Ĵ����߼���</description></item>
    /// <item><description>������Ϸ����������ִ����Ʒ�Ĳ��������������Ľ�����</description></item>
    /// <item><description>��ʵ�ֶ��ص����������߼�ʱ���������͵���¼���</description></item>
    /// <item><description>ͨ��ж�������ѹҹ����¼���ȷ�����������ڴ�й©��</description></item>
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

                if (relicItem.IsValidRelic(out ModRelic relic1))// ���Ҽ�ʱ�����������λ��������Ϊ�գ����������������λ����
                {
                    relic1.SetToNoSet();
                    relic1.UpdateValue();
                    Item lastRelicItem = relicItem.Clone();

                    relicUnitSlots[(int)relic.RelicType].relicItem = clickItem.Clone();
                    inv[slot] = lastRelicItem;
                }
                else// ���Ҽ�ʱ���������Ϊ�գ���λ����Ϊ�գ������������
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
