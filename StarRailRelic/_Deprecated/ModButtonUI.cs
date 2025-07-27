/*using Humanizer;

namespace StarRailRelic.Common.UI
{
    public abstract class ModButtonUI : UIHandle
    {
        public sealed override Texture2D Texture => Mod.Assets.Request<Texture2D>("Assets/Textures/" + TexturePath).Value;
        public override bool Active => Main.playerInventory;
        public string TexturePath { get; set; } = "";

        public bool LeftButtonDown => IsMouseHovering && keyLeftPressState == KeyPressState.Pressed;
        public bool LeftButtonUp => IsMouseHovering && keyLeftPressState == KeyPressState.Released;
        public bool RightButtonDown => IsMouseHovering && keyRightPressState == KeyPressState.Pressed;
        public bool RightButtonUp => IsMouseHovering && keyRightPressState == KeyPressState.Released;
        public bool IsMouseHovering => UIHitBox.Contains(new Rectangle(Main.mouseX, Main.mouseY, 1, 1));

        private bool playedHoverSound;

        private Vector2 offset;
        private bool dragging;

        public bool Dragable { get; set; }

        public sealed override void Update()
        {
            if (LeftButtonDown)
            {
                OnLeftButtonDown();
            }
            if (LeftButtonUp)
            {
                OnLeftButtonUp();
            }
            if (RightButtonDown)
            {
                _OnRightButtonDown();
            }
            if (RightButtonUp)
            {
                _OnRightButtonUp();
            }

            UpdateUI();

            if (IsMouseHovering)
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

            dragging = Dragable && IsMouseHovering && keyRightPressState == KeyPressState.Held;
            if (dragging)
            {
                DrawPosition.X = Main.mouseX - offset.X;
                DrawPosition.Y = Main.mouseY - offset.Y;
            }

            UIHitBox = new Rectangle((int)DrawPosition.X, (int)DrawPosition.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public virtual void UpdateUI() { }

        public virtual void OnLeftButtonDown() { }
        public virtual void OnLeftButtonUp() { }

        public virtual void OnRightButtonDown() { }
        public virtual void OnRightButtonUp() { }

        private void _OnRightButtonDown()
        {
            if (Dragable)
            {
                DragStart();
            }

            OnRightButtonDown();
        }

        private void _OnRightButtonUp()
        {
            if (Dragable)
            {
                DragEnd();
            }

            OnRightButtonUp();
        }

        /// <summary>
        /// 开始拖动
        /// </summary>
        private void DragStart()
        {
            //保证面板随着鼠标平滑移动
            offset = new Vector2(Main.mouseX - DrawPosition.X, Main.mouseY - DrawPosition.Y);
        }

        /// <summary>
        /// 结束拖动
        /// </summary>
        private void DragEnd()
        {
            DrawPosition.X = Main.mouseX - offset.X;
            DrawPosition.Y = Main.mouseY - offset.Y;
        }
    }
}
*/