namespace StarRailRelic.Common.UI
{
    public abstract class ModButtonUI : UIHandle
    {
        public sealed override Texture2D Texture => Mod.Assets.Request<Texture2D>("Assets/Textures/" + TexturePath).Value;
        public override bool Active => Main.playerInventory;
        public string TexturePath = "";

        public bool LeftButtonClicked => IsMouseHovering && keyLeftPressState == KeyPressState.Released;
        public bool IsMouseHovering => UIHitBox.Contains(new Rectangle(Main.mouseX, Main.mouseY, 1, 1));

        private bool playedHoverSound;

        public sealed override void Update()
        {
            UpdateUI();
            UIHitBox = new Rectangle((int)DrawPosition.X, (int)DrawPosition.Y, Texture.Width, Texture.Height);

            if (UIHitBox.Contains(new Rectangle(Main.mouseX, Main.mouseY, 1, 1)))
            {
                if (!playedHoverSound)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    playedHoverSound = true;
                }
            }
            else
            {
                playedHoverSound = false;
            }

            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (LeftButtonClicked)
            {
                OnLeftButtonClicked();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public virtual void UpdateUI() { }

        public virtual void OnLeftButtonClicked() { }
    }
}
