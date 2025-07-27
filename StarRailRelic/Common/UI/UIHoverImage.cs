namespace StarRailRelic.Common.UI
{
    public class UIHoverImage : UIImage
    {
        public string hoverText;

        public event MouseEvent OnMouseHoverButton;
        public event MouseEvent OnLeftClickButton;
        public event MouseEvent OnLeftDoubleClickButton;
        public event MouseEvent OnRightDoubleClickButton;

        private Texture2D texture;
        private Texture2D hoverTexture;
        private bool ishoverTextTooltip;

        public UIHoverImage(Texture2D texture, Texture2D hoverTexture, string hoverText = null) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            this.hoverTexture = hoverTexture;
        }

        public UIHoverImage(Texture2D texture, string hoverText = null) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            hoverTexture = texture;
        }

        public void Set(Texture2D texture, Texture2D hoverTexture, string hoverText = null, bool ishoverTextTooltip = false)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            this.hoverTexture = hoverTexture;
            this.ishoverTextTooltip = ishoverTextTooltip;
        }

        public void Set(Texture2D texture, string hoverText = null)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            hoverTexture = texture;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
                spriteBatch.Draw(hoverTexture, GetDimensions().Position(), null, Color.White, 0f, Vector2.Zero, ImageScale, SpriteEffects.None, 0);

                if (ishoverTextTooltip)
                {
                    UICommon.TooltipMouseText(hoverText);
                }
                else
                {
                    Main.hoverItemName = hoverText;
                }
            }
            else
            {
                spriteBatch.Draw(texture, GetDimensions().Position(), null, Color.White, 0f, Vector2.Zero, ImageScale, SpriteEffects.None, 0);
            }
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            IsMouseHovering = true;
            OnMouseHoverButton?.Invoke(evt, this);
            Parent?.MouseOver(evt);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            OnLeftClickButton?.Invoke(evt, this);
            Parent?.LeftClick(evt);
        }

        public override void LeftDoubleClick(UIMouseEvent evt)
        {
            OnLeftDoubleClickButton?.Invoke(evt, this);
            Parent?.LeftDoubleClick(evt);
        }

        public override void RightDoubleClick(UIMouseEvent evt)
        {
            OnRightDoubleClickButton?.Invoke(evt, this);
            Parent?.RightDoubleClick(evt);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}
