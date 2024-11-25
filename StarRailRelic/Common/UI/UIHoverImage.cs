namespace StarRailRelic.Common.UI
{
    public class UIHoverImage : UIImage
    {
        public string hoverText;

        public event MouseEvent OnLeftClickButton;
        public event MouseEvent OnLeftDoubleClickButton;
        public event MouseEvent OnRightDoubleClickButton;

        private readonly Asset<Texture2D> texture;
        private readonly Asset<Texture2D> hoverTexture;

        public UIHoverImage(Asset<Texture2D> texture, Asset<Texture2D> hoverTexture, string hoverText = null) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            this.hoverTexture = hoverTexture;
        }

        public UIHoverImage(Asset<Texture2D> texture, string hoverText = null) : base(texture)
        {
            this.hoverText = hoverText;
            this.texture = texture;
            hoverTexture = texture;
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
