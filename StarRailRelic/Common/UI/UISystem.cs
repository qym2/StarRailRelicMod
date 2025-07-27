namespace StarRailRelic.Common.UI
{
    /// <summary>
    /// 表示一个通用的用户界面系统，通过泛型参数 <typeparamref name="T"/> 指定具体的 UI 状态类型。
    /// 此系统负责显示和隐藏 UI，管理 UI 的状态，同时支持音效播放和更新 UI 的逻辑。
    /// </summary>
    /// <typeparam name="T">用于指定 UI 状态的类型，必须是 <see cref="UIState"/> 的子类，并且有一个无参数构造函数。</typeparam>
    [Autoload(Side = ModSide.Client)]
    public class UISystem<T> : ModSystem where T : UIState, new()
    {
        private readonly UserInterface userInterface = new();
        public readonly T uiState = new();

        public bool isUIOpen;

        protected bool showAlways;
        protected bool showInInventory;
        protected virtual bool PlayOpenSound => true;
        protected virtual bool PlayCloseSound => true;

        public Vector2? ActiveTilePosition { get; set; }

        public virtual void ShowUI()
        {
            userInterface?.SetState(uiState);

            if (!isUIOpen)
            {
                if (PlayOpenSound)
                {
                    _ = SoundEngine.PlaySound(SoundID.MenuOpen);
                }
                isUIOpen = true;
            }
        }

        public virtual void HideUI()
        {
            userInterface?.SetState(null);

            if (isUIOpen)
            {
                if (PlayCloseSound)
                {
                    _ = SoundEngine.PlaySound(SoundID.MenuClose);
                }
                isUIOpen = false;
                ActiveTilePosition = null;
            }
        }

        public override void Load()
        {
            uiState.Activate();
        }

        public sealed override void UpdateUI(GameTime gameTime)
        {
            if (userInterface?.CurrentState != null)
            {
                userInterface?.Update(gameTime);
            }

            Update();
        }

        protected virtual void Update()
        {
            if (showAlways)
            {
                ShowUI();
            }
            else
            {
                if (showInInventory)
                {
                    if (Main.playerInventory)
                    {
                        ShowUI();
                    }
                    else
                    {
                        HideUI();
                    }

                    if (Main.LocalPlayer.chest != -1)
                    {
                        HideUI();
                    }
                }
            }//Main.NewText(Main.LocalPlayer.inventory[0].Name);

            if (ActiveTilePosition != null)
            {
                Vector2 playerPos = Main.LocalPlayer.Center;
                Vector2 tilePos = ActiveTilePosition.Value;

                if (Vector2.Distance(tilePos, playerPos) / 16f > 8f)
                {
                    HideUI();
                }
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int Index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (Index != -1)
            {
                layers.Insert(Index, new LegacyGameInterfaceLayer(
                    $"StarRailRelic: {typeof(T).Name}",
                    delegate
                    {
                        if (userInterface?.CurrentState != null)
                        {
                            userInterface.Draw(Main.spriteBatch, new GameTime());
                        }

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}