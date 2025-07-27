/*namespace StarRailRelic.Content.Items
{
    public class RelicGuide : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = RarityType<BlueRarity>();
            Item.value = Item.sellPrice(0, 0, 12, 0);

            Item.width = 40;
            Item.height = 38;

            Item.useTime = 30;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            RelicDisplayUISystem uiSystem = GetInstance<RelicDisplayUISystem>();
            if (player.altFunctionUse == 2)
            {
                uiSystem.HideUI();
                return false;
            }
            if (uiSystem.isUIOpen)
            {
                uiSystem.HideUI();
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            GetInstance<RelicDisplayUISystem>().ShowUI();

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient<LostCrystal>()
                .Register();
        }
    }
}*/
namespace StarRailRelic.Common.UI.Relic {     /// <summary>     /// 遗器主用户界面系统类，负责管理和显示遗器相关的用户界面.     /// 该类会在客户端自动加载，并负责创建和更新遗器的UI状态。     /// </summary>     [Autoload(Side = ModSide.Client)]     public class RelicMainUISystem : UISystem<RelicMainUI>     {         protected override void Update()         {             showInInventory = PersonalConfigs.Instance.HideRelicButtonUI;              base.Update();         }     }      /// <summary>     /// 遗器主用户界面状态类，负责管理玩家的遗器栏位和相关的用户界面逻辑。     /// </summary>     /// <remarks>     /// 此类通过多个 <see cref="RelicUnitSlot"/> 实例来展示玩家所装备的遗器，每个槽位对应一个遗器类型。     /// <para>主要功能包括：</para>     /// <list type="bullet">     /// <item><description>初始化并配置遗器槽位的显示，包括遗器的图标展示和信息提示。</description></item>     /// <item><description>更新遗器槽位的文字说明。</description></item>     /// <item><description>同步与玩家的遗器数据，包括保存和加载遗器的状态。</description></item>     /// </list>     /// </remarks>     public class RelicMainUI : UIState     {         public readonly RelicUnitSlot slot1 = new(RelicType.Head);         public readonly RelicUnitSlot slot2 = new(RelicType.Hands);         public readonly RelicUnitSlot slot3 = new(RelicType.Body);         public readonly RelicUnitSlot slot4 = new(RelicType.Feet);         public readonly RelicUnitSlot slot5 = new(RelicType.PlanarSphere);         public readonly RelicUnitSlot slot6 = new(RelicType.LinkRope);          private readonly UIText text1 = new("");         private readonly UIText text2 = new("");         private readonly UIText text3 = new("");         private readonly UIText text4 = new("");         private readonly UIText text5 = new("");         private readonly UIText text6 = new("");          public override void OnInitialize()         {             // 初始化遗器栏位ui             InitializeSlot(slot1, text1, 780, 300, 12);             InitializeSlot(slot2, text2, 780, 650, 12);             InitializeSlot(slot3, text3, 1130, 670, 12);             InitializeSlot(slot4, text4, 1130, 320, 12);             InitializeSlot(slot5, text5, 900, 540, 3);             InitializeSlot(slot6, text6, 1010, 430, 3);         }          private void InitializeSlot(RelicUnitSlot slot, UIText text, int left, int top, int textLeft)         {             slot.SetRectangle(left: left, top: top, width: 64, height: 64);             text.Top.Set(slot.Height.Pixels + 8, 0f);             text.Left.Set(textLeft, 0f);             slot.Append(text);             Append(slot);         }          public override void Update(GameTime gameTime)         {             base.Update(gameTime);              // 更新栏位文字说明             text1.SetText(HeadRelicText);             text2.SetText(HandsRelicText);             text3.SetText(BodyRelicText);             text4.SetText(FeetRelicText);             text5.SetText(PlanarSphereRelicText);             text6.SetText(LinkRopeRelicText);              SyncRelicData();         }          public void SyncRelicData()         {             // 同步栏位遗器数据             RelicPlayer player = Main.LocalPlayer.GetModPlayer<RelicPlayer>();             LoadData(player);         }          private void LoadData(RelicPlayer player)         {             if (player.HeadRelic != null && player.HeadRelic.stack > 0)             {                 slot1.relicItem = player.HeadRelic;             }             if (player.HandsRelic != null && player.HandsRelic.stack > 0)             {                 slot2.relicItem = player.HandsRelic;             }             if (player.BodyRelic != null && player.BodyRelic.stack > 0)             {                 slot3.relicItem = player.BodyRelic;             }             if (player.FeetRelic != null && player.FeetRelic.stack > 0)             {                 slot4.relicItem = player.FeetRelic;             }             if (player.PlanarSphereRelic != null && player.PlanarSphereRelic.stack > 0)             {                 slot5.relicItem = player.PlanarSphereRelic;             }             if (player.LinkRopeRelic != null && player.LinkRopeRelic.stack > 0)             {                 slot6.relicItem = player.LinkRopeRelic;             }         }     } } 