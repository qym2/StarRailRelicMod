namespace StarRailRelic.Common.UI
{
    /// <summary>
    /// 代表遗器的单位栏位，用于显示和管理玩家装备的遗器。
    /// 该类从 <see cref="UIImageButton"/> 继承，提供对遗器的交互功能，包括显示遗器图标、处理鼠标操作（左键和右键），
    /// 以及管理与玩家背包之间的遗器交换。
    /// <para>主要功能包括：</para>
    /// <list type="bullet">
    /// <item><description>根据遗器类型展示相关的遗器图片。</description></item>
    /// <item><description>处理鼠标悬浮时显示提示信息。</description></item>
    /// <item><description>通过右键快速将遗器取出并放入玩家背包。</description></item>
    /// <item><description>通过左键操作交换或放置遗器。</description></item>
    /// </list>
    /// </summary>
    public class RelicUnitSlot(RelicType slotType) : UIImageButton(RelicSlotTexture)
    {
        private readonly RelicType slotType = slotType;

        private Asset<Texture2D> relicTexture;

        public Item relicItem;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (relicItem != null)
            {
                // 有遗器显示遗器图片
                relicTexture = TextureAssets.Item[relicItem.type];
            }
            else
            {
                // 无遗器显示为空
                relicTexture = NullTexture;
            }

            // 根据遗器图片大小计算遗器图片位置
            float relicTextureX = GetDimensions().X + (GetDimensions().Width - relicTexture.Value.Width) / 2;
            float relicTextureY = GetDimensions().Y + (GetDimensions().Height - relicTexture.Value.Height) / 2;

            spriteBatch.Draw(RelicSlotTexture.Value, GetDimensions().Position(), Color.White);
            spriteBatch.Draw(relicTexture.Value, new Vector2(relicTextureX, relicTextureY), Color.White);

            if (IsMouseHovering)
            {
                if (relicItem != null)
                {
                    // 鼠标悬浮在遗器栏位上时显示物品提示
                    Main.hoverItemName = relicItem.Clone().Name;
                    Main.HoverItem = relicItem.Clone();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // 鼠标悬浮在ui时禁用玩家输入
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Terraria.Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Terraria.Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

                Recalculate();
            }
        }

        public override void RightClick(UIMouseEvent evt)
        {
            // 若右键时栏位遗器不为空，则快捷取出遗器至玩家背包最前一格
            if (relicItem.IsValidRelic(out ModRelic relic))
            {
                Player player = Main.LocalPlayer;
                int emptySlotIndex = player.GetFirstItemIndex(0);
                if (emptySlotIndex != -1)
                {
                    relic.SetToNoSet();
                    relic.UpdateValue();
                    player.inventory[emptySlotIndex] = relicItem.Clone();
                    relicItem.SetDefaults(0);

                    SoundEngine.PlaySound(SoundID.Grab);
                }
            }
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Item mouseItem = Main.mouseItem;

            if (mouseItem.IsValidRelic(out ModRelic relic) && (relic.RelicType == slotType || slotType == RelicType.None))
            {
                if (relicItem.IsValidRelic(out ModRelic relic1))// 若左键时鼠标遗器和栏位遗器均不为空，交换鼠标遗器和栏位遗器
                {
                    relic1.SetToNoSet();
                    relic1.UpdateValue();
                    Item lastRelicItem = relicItem.Clone();

                    relicItem = mouseItem.Clone();
                    Main.mouseItem = lastRelicItem;
                }
                else// 若左键时鼠标遗器不为空，栏位遗器为空，放置鼠标遗器
                {
                    relicItem = mouseItem.Clone();

                    mouseItem.SetDefaults();
                }

                relic.UpdateValue();
                SoundEngine.PlaySound(SoundID.Grab);

                if(slotType == RelicType.None)
                {
                    Main.NewText(RelicInforText.WithFormatArgs(relicItem.Name, relic.level), Color.YellowGreen);
                    if (relic.level < relic.levelMax)
                    {
                        Main.NewText(NextLevelItemCountText.WithFormatArgs(ModRelic.RelicStrengtheningRequiredItemCount[relic.level]), Color.YellowGreen);
                    }
                    else
                    {
                        Main.NewText(RelicLevelMaxText, Color.Red);
                    }
                }
            }
            else if (relic != null && relic.RelicType != slotType)
            {

            }
            else
            {
                if (relicItem.IsValidRelic(out ModRelic relic1))// 若左键时鼠标遗器为空，栏位遗器不为空，取出遗器至鼠标
                {
                    relic1.SetToNoSet();
                    relic1.UpdateValue();
                    Main.mouseItem = relicItem.Clone();

                    relicItem.SetDefaults();

                    SoundEngine.PlaySound(SoundID.Grab);
                }
            }
        }
    }
}
