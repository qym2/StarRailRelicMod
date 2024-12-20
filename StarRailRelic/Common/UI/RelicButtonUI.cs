namespace StarRailRelic.Common.UI
{
    /// <summary>
    /// 遗器按钮用户界面系统类，负责管理遗器按钮的用户界面逻辑。
    /// 该类在客户端自动加载，不播放打开或关闭声音效果。
    /// </summary>
    [Autoload(Side = ModSide.Client)]
    public class RelicButtonUISystem : UISystem<RelicButtonUI>
    {
        protected override bool PlayCloseSound => false;
        protected override bool PlayOpenSound => false;

        public override void Load()
        {
            base.Load();
            showInInventory = true;
        }

        protected override void Update()
        {
            base.Update();

            if (PersonalConfigs.Instance.HideRelicButtonUI)
            {
                HideUI();
            }
        }
    }

    /// <summary>
    /// 遗器按钮用户界面状态类，处理主菜单按钮的初始化和操作。
    /// 包含一个可点击的按钮和对应的文本。
    /// </summary>
    public class RelicButtonUI : UIState
    {
        private UIHoverImageButton mainMenuButton;

        public override void OnInitialize()
        {
            mainMenuButton = new(RelicButtonTexture, RelicButton_HoverTexture, dragable: true);
            mainMenuButton.SetRectangle(left: 580f, top: 255f, width: 32f, height: 36f);
            mainMenuButton.OnLeftClickButton += ButtonClicked;

            Append(mainMenuButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (mainMenuButton.IsMouseHovering)
            {
                UICommon.TooltipMouseText(RelicButtonText);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GetInstance<RelicMainUISystem>().uiState.SyncRelicData();
        }

        private static void ButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            // 未打开则打开装备遗器ui，打开则关闭
            if (!GetInstance<RelicMainUISystem>().isUIOpen)
            {
                GetInstance<RelicMainUISystem>().ShowUI();
            }
            else
            {
                GetInstance<RelicMainUISystem>().HideUI();
            }
        }
    }
}
