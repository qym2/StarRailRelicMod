namespace StarRailRelic.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    public class TrailblazePowerUISystem : UISystem<TrailblazePowerUI>
    {
        protected override bool PlayCloseSound => false;
        protected override bool PlayOpenSound => false;

        public override void Load()
        {
            base.Load();
            showInInventory = true;
        }
    }

    public class TrailblazePowerUI : UIState
    {
        private UIHoverImage image;
        private readonly UIText text = new("");

        public override void OnInitialize()
        {
            image = new(TrailblazePowerTexture);
            image.SetRectangle(left: 580, top: 80, width: 35, height: 36);
            text.Top.Set(11, 0f);
            text.Left.Set(40, 0f);
            image.Append(text);
            Append(image);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (image.IsMouseHovering)
            {
                UICommon.TooltipMouseText(TrailblazePowerTipText);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text.SetText($"{Main.LocalPlayer.GetModPlayer<TrailblazePowerPlayer>().TrailblazePower}");
        }
    }
}
