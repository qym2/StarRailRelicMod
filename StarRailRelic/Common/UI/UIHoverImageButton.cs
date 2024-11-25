namespace StarRailRelic.Common.UI
{
    /// <summary>
    /// 支持鼠标悬浮时改变显示的图像及显示提示文本的图像按钮。
    /// 还支持拖动功能，可以在父容器内自由移动。
    /// </summary>
    public class UIHoverImageButton : UIImageButton
    {
        public string hoverText;

        public event MouseEvent OnLeftClickButton;

        private readonly Asset<Texture2D> texture;
        private readonly Asset<Texture2D> hoverTexture;

        private readonly float opacity;

        private Vector2 offset;
        private bool dragging;

        private readonly bool gragable;

        public UIHoverImageButton(Asset<Texture2D> texture, Asset<Texture2D> hoverTexture, string hoverText = null, float opacity = 0.8f, bool gragable = false) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            this.hoverTexture = hoverTexture;
            this.opacity = opacity;
            this.gragable = gragable;
        }

        public UIHoverImageButton(Asset<Texture2D> texture, string hoverText = null, float opacity = 0.8f, bool gragable = false) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            hoverTexture = texture;
            this.opacity = opacity;
            this.gragable = gragable;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
                spriteBatch.Draw(hoverTexture.Value, GetDimensions().Position(), Color.White);
                Main.hoverItemName = hoverText;
            }
            else
            {
                spriteBatch.Draw(texture.Value, GetDimensions().Position(), Color.White);
            }
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            OnLeftClickButton?.Invoke(evt, this);
            Parent?.LeftClick(evt);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (dragging)
            {
                // Main.MouseScreen.X 就是 Main.mouseX
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }

            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Terraria.Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Terraria.Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);

                Recalculate();
            }
        }
        public override void MiddleMouseDown(UIMouseEvent evt)
        {
            base.MiddleMouseDown(evt);

            if (gragable)
            {
                DragStart(evt);
            }
        }

        public override void MiddleMouseUp(UIMouseEvent evt)
        {
            base.MiddleMouseUp(evt);

            if (gragable)
            {
                DragEnd(evt);
            }
        }

        /// <summary>
        /// 开始拖动
        /// </summary>
        private void DragStart(UIMouseEvent evt)
        {
            //保证面板随着鼠标平滑移动
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        /// <summary>
        /// 结束拖动
        /// </summary>
        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 endMousePosition = evt.MousePosition;
            dragging = false;

            Left.Set(endMousePosition.X - offset.X, 0f);
            Top.Set(endMousePosition.Y - offset.Y, 0f);

            Recalculate();
        }
    }
}
