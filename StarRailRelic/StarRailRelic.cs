using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StarRailRelic.Content.Items.Weapons.Melee;
using Terraria.UI.Gamepad;

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
            On_Main.DrawCursor += On_Main_DrawCursor;
            On_Main.DrawThickCursor += On_Main_DrawThickCursor;
        }

        private void On_ItemSlot_RightClick_ItemArray_int_int(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
        {
            orig.Invoke(inv, context, slot);
            SwapRelic(inv, context, slot);
        }

        private void On_Player_HealEffect(On_Player.orig_HealEffect orig, Player self, int healAmount, bool broadcast)
        {
            orig.Invoke(self, healAmount, broadcast);

            if (healAmount >= 50)
            {
                self.GetModPlayer<RelicSetSpecialEffectPlayer>().OnHealLife();
            }
        }

        private void On_Player_ManaEffect(On_Player.orig_ManaEffect orig, Player self, int manaAmount)
        {
            orig.Invoke(self, manaAmount);

            if (manaAmount >= 50)
            {
                self.GetModPlayer<RelicSetSpecialEffectPlayer>().OnHealMana();
            }
        }

        private void On_Main_DrawCursor(On_Main.orig_DrawCursor orig, Vector2 bonus, bool smart)
        {
            if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<CommonAccessoryPlayer>().IsStarRailCursor)
            {
                Texture2D myCustomCursorTexture = Request<Texture2D>("StarRailRelic/Assets/Textures/cusor").Value;

                if (Main.gameMenu && Main.alreadyGrabbingSunOrMoon)
                    return;

                if (Main.player[Main.myPlayer].dead || Main.player[Main.myPlayer].mouseInterface)
                {
                    Main.ClearSmartInteract();
                    Main.TileInteractionLX = (Main.TileInteractionHX = (Main.TileInteractionLY = (Main.TileInteractionHY = -1)));
                }

                bool flag = UILinkPointNavigator.Available && !PlayerInput.InBuildingMode;
                if (PlayerInput.SettingsForUI.ShowGamepadCursor)
                {
                    if ((Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost && !Main.gameMenu) || PlayerInput.InvisibleGamepadInMenus)
                        return;

                    Vector2 t = new(Main.mouseX, Main.mouseY);
                    Vector2 t2 = Vector2.Zero;
                    bool flag2 = Main.SmartCursorIsUsed;
                    if (flag2)
                    {
                        PlayerInput.smartSelectPointer.UpdateCenter(Main.ScreenSize.ToVector2() / 2f);
                        t2 = PlayerInput.smartSelectPointer.GetPointerPosition();
                        if (Vector2.Distance(t2, t) < 1f)
                            flag2 = false;
                        else
                            Terraria.Utils.Swap(ref t, ref t2);
                    }

                    if (flag2)
                    {
                        Main.spriteBatch.Draw(myCustomCursorTexture, t2 + bonus, null, Color.White, (float)Math.PI / 2f * Main.GlobalTimeWrappedHourly, myCustomCursorTexture.Size() / 2f, Main.cursorScale, SpriteEffects.None, 0f);
                    }

                    if (smart && !flag)
                    {
                        Main.spriteBatch.Draw(myCustomCursorTexture, t + bonus, null, Color.White, 0f, myCustomCursorTexture.Size() / 2f, Main.cursorScale, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        Main.spriteBatch.Draw(myCustomCursorTexture, new Vector2(Main.mouseX, Main.mouseY) + bonus, null, Color.White, 0f, myCustomCursorTexture.Size() / 2f, Main.cursorScale, SpriteEffects.None, 0f);
                    }
                }
                else
                {
                    if (smart && !flag)
                    {
                        Main.spriteBatch.Draw(myCustomCursorTexture, new Vector2(Main.mouseX, Main.mouseY) + bonus + Vector2.One, null, Color.Black, 0f, new(1.9f, 2.3f), Main.cursorScale * 1.3f, SpriteEffects.None, 0f);
                        Main.spriteBatch.Draw(myCustomCursorTexture, new Vector2(Main.mouseX, Main.mouseY) + bonus, null, Color.White, 0f, default, Main.cursorScale * 1.2f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        Main.spriteBatch.Draw(myCustomCursorTexture, new Vector2(Main.mouseX, Main.mouseY) + bonus + Vector2.One, null, Color.Black, 0f, new(2, 2), Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
                        Main.spriteBatch.Draw(myCustomCursorTexture, new Vector2(Main.mouseX, Main.mouseY) + bonus, null, Color.White, 0f, default, Main.cursorScale, SpriteEffects.None, 0f);
                    }
                }
            }
            else
            {
                orig.Invoke(bonus, smart);
            }
        }

        private Vector2 On_Main_DrawThickCursor(On_Main.orig_DrawThickCursor orig, bool smart)
        {
            if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<CommonAccessoryPlayer>().IsStarRailCursor)
            {
                if (Main.ThickMouse)
                {
                    bool showGamepadCursor = PlayerInput.SettingsForUI.ShowGamepadCursor;
                    if (Main.gameMenu && Main.alreadyGrabbingSunOrMoon)
                        return Vector2.Zero;

                    if (showGamepadCursor && PlayerInput.InvisibleGamepadInMenus)
                        return Vector2.Zero;

                    if (showGamepadCursor && Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost && !Main.gameMenu)
                        return Vector2.Zero;

                    return new Vector2(2f);
                }

                return Vector2.Zero;
            }
            else
            {
                return orig.Invoke(smart);
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
                    RelicUnitSlot.UpdatePlayerRelic(relic.RelicType, relicUnitSlots[(int)relic.RelicType].relicItem);
                    inv[slot] = lastRelicItem;
                }
                else// ���Ҽ�ʱ���������Ϊ�գ���λ����Ϊ�գ������������
                {
                    relicUnitSlots[(int)relic.RelicType].relicItem = clickItem.Clone();
                    RelicUnitSlot.UpdatePlayerRelic(relic.RelicType, relicUnitSlots[(int)relic.RelicType].relicItem);

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
            On_Main.DrawCursor -= On_Main_DrawCursor;
            On_Main.DrawThickCursor -= On_Main_DrawThickCursor;
        }
    }
}
