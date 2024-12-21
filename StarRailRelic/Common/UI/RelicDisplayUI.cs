using StarRailRelic.Content.Items.Relic.In.One.Banditry;
using StarRailRelic.Content.Items.Relic.In.One.Belobog;
using StarRailRelic.Content.Items.Relic.In.One.Differentiator;
using StarRailRelic.Content.Items.Relic.In.One.Enterprise;
using StarRailRelic.Content.Items.Relic.In.One.Glamoth;
using StarRailRelic.Content.Items.Relic.In.One.Keel;
using StarRailRelic.Content.Items.Relic.In.One.Penacony;
using StarRailRelic.Content.Items.Relic.In.One.Rutilant;
using StarRailRelic.Content.Items.Relic.In.One.Salsotto;
using StarRailRelic.Content.Items.Relic.In.One.Space;
using StarRailRelic.Content.Items.Relic.In.One.Vonwacq;
using StarRailRelic.Content.Items.Relic.In.One.Xianzhou;
using StarRailRelic.Content.Items.Relic.In.Two.Banana;
using StarRailRelic.Content.Items.Relic.In.Two.Izumo;
using StarRailRelic.Content.Items.Relic.In.Two.Kalpagni;
using StarRailRelic.Content.Items.Relic.In.Two.Sigonia;
using StarRailRelic.Content.Items.Relic.In.Two.Sunken;
using StarRailRelic.Content.Items.Relic.In.Two.Wolves;
using StarRailRelic.Content.Items.Relic.Out.One.Fire;
using StarRailRelic.Content.Items.Relic.Out.One.Guard;
using StarRailRelic.Content.Items.Relic.Out.One.Healing;
using StarRailRelic.Content.Items.Relic.Out.One.Ice;
using StarRailRelic.Content.Items.Relic.Out.One.Imaginary;
using StarRailRelic.Content.Items.Relic.Out.One.Knight;
using StarRailRelic.Content.Items.Relic.Out.One.Life;
using StarRailRelic.Content.Items.Relic.Out.One.Lightning;
using StarRailRelic.Content.Items.Relic.Out.One.Messenger;
using StarRailRelic.Content.Items.Relic.Out.One.Musketeer;
using StarRailRelic.Content.Items.Relic.Out.One.Physics;
using StarRailRelic.Content.Items.Relic.Out.One.Quantum;
using StarRailRelic.Content.Items.Relic.Out.One.Thief;
using StarRailRelic.Content.Items.Relic.Out.One.Wind;
using StarRailRelic.Content.Items.Relic.Out.Two.DeadWaters;
using StarRailRelic.Content.Items.Relic.Out.Two.Dot;
using StarRailRelic.Content.Items.Relic.Out.Two.Duke;
using StarRailRelic.Content.Items.Relic.Out.Two.Feixiao;
using StarRailRelic.Content.Items.Relic.Out.Two.Iron;
using StarRailRelic.Content.Items.Relic.Out.Two.Sacerdos;
using StarRailRelic.Content.Items.Relic.Out.Two.Scholar;
using StarRailRelic.Content.Items.Relic.Out.Two.Watch;

namespace StarRailRelic.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    public class RelicDisplayUISystem : UISystem<RelicDisplayUI>
    {
        public override void ShowUI()
        {
            base.ShowUI();

            uiState.InitializeUI();
            Main.playerInventory = false;
        }
    }

    public class RelicDisplayUI : UIState
    {
        private readonly List<RelicUnitSlot> slotList =
        [
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),

            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),

            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),

            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display),
            new(RelicType.Display)
        ];

        private readonly UIText titalText = new("");

        private readonly UIPanel panel = new();

        public override void OnInitialize()
        {
            panel.SetRectangle(896, 286, -40);
            panel.SetPadding(0);
            panel.BackgroundColor = new Color(73, 94, 171, 175);

            titalText.Width.Set(panel.Width.Pixels, 0);
            titalText.Top.Set(10, 0);
            panel.Append(titalText);

            int[] topPositions = [0, 64, 128, 192];
            int[] counts = [14, 22, 34, 40];
            int startIndex = 0;

            for (int j = 0; j < topPositions.Length; j++)
            {
                int top = topPositions[j] + 30;
                int count = counts[j];

                for (int i = startIndex; i < count; i++)
                {
                    int left = (i - startIndex) * 64;
                    slotList[i].SetRectangle(left: left, top: top, width: 64, height: 64);
                    panel.Append(slotList[i]);
                }

                startIndex = count;
            }


            Append(panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            SetRelicDisplayType<HealingHead>(0);
            SetRelicDisplayType<MusketeerHead>(1);
            SetRelicDisplayType<KnightHead>(2);
            SetRelicDisplayType<IceHead>(3);
            SetRelicDisplayType<PhysicsHead>(4);
            SetRelicDisplayType<GuardHead>(5);
            SetRelicDisplayType<FireHead>(6);
            SetRelicDisplayType<QuantumHead>(7);
            SetRelicDisplayType<LightningHead>(8);
            SetRelicDisplayType<WindHead>(9);
            SetRelicDisplayType<ThiefHead>(10);
            SetRelicDisplayType<ImaginaryHead>(11);
            SetRelicDisplayType<LifeHead>(12);
            SetRelicDisplayType<MessengerHead>(13);

            SetRelicDisplayType<DukeHead>(14);
            SetRelicDisplayType<DotHead>(15);
            SetRelicDisplayType<DeadWatersHead>(16);
            SetRelicDisplayType<WatchHead>(17);
            SetRelicDisplayType<IronHead>(18);
            SetRelicDisplayType<FeixiaoHead>(19);
            SetRelicDisplayType<SacerdosHead>(20);
            SetRelicDisplayType<ScholarHead>(21);

            SetRelicDisplayType<SpaceSphere>(22);
            SetRelicDisplayType<XianzhouSphere>(23);
            SetRelicDisplayType<EnterpriseSphere>(24);
            SetRelicDisplayType<BelobogSphere>(25);
            SetRelicDisplayType<DifferentiatorSphere>(26);
            SetRelicDisplayType<SalsottoSphere>(27);
            SetRelicDisplayType<BanditrySphere>(28);
            SetRelicDisplayType<VonwacqSphere>(29);
            SetRelicDisplayType<RutilantSphere>(30);
            SetRelicDisplayType<KeelSphere>(31);
            SetRelicDisplayType<GlamothSphere>(32);
            SetRelicDisplayType<PenaconySphere>(33);

            SetRelicDisplayType<SigoniaSphere>(34);
            SetRelicDisplayType<IzumoSphere>(35);
            SetRelicDisplayType<WolvesSphere>(36);
            SetRelicDisplayType<KalpagniSphere>(37);
            SetRelicDisplayType<SunkenSphere>(38);
            SetRelicDisplayType<BananaSphere>(39);
        }

        public void InitializeUI()
        {
            titalText.SetText(RelicDisplayUITital);
        }

        private void SetRelicDisplayType<T>(int index) where T : ModRelic
        {
            if (!slotList[index].relicItem.IsValidRelic())
            {
                slotList[index].relicItem = Main.item[Item.NewItem(null, new Vector2(-100, -100), ItemType<T>())].Clone();

                if (slotList[index].relicItem.ModItem is ModRelic modRelic)
                {
                    modRelic.isDisplay = true;
                }
            }
        }
    }
}
