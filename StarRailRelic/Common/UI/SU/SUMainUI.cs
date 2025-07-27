namespace StarRailRelic.Common.UI.SU {     [Autoload(Side = ModSide.Client)]     public class SUMainUISystem : UISystem<SUMainUI>     {         public override void ShowUI()         {             uiState.InitializeUI();             uiState.activeTilePosition = ActiveTilePosition.Value;              Main.playerInventory = false;              base.ShowUI();         }
    }      public class SUMainUI : UIState     {         public DragableUIPanel mainMenu = new(false);         public DragableUIPanel[] worldpanels = [.. Enumerable.Range(0, 9).Select(_ => new DragableUIPanel(false))];         public ClickableUIText[] worldtexts = [.. Enumerable.Range(0, 9).Select(_ => new ClickableUIText("", 0, 5))];         public ClickableUIText startButton;          public UIText bossListText = new("");         public UIText enemyListText = new("");         public UIText difficultyLabel = new("");         public UIText difficultyValue = new("");
        public UIText enemyTitle = new("");
        public UIText rewardTitle = new("");

        private readonly UIScrollbar scrollbar = new();
        private readonly UIElement contentContainer = new();

        public UIHoverImage[] bossImages = [.. Enumerable.Range(0, 5).Select(_ => new UIHoverImage(NullTexture.Value))];         public UIHoverImage[] enemyImages = [.. Enumerable.Range(0, 16).Select(_ => new UIHoverImage(NullTexture.Value))];         public UIHoverImage[] awardImages = [.. Enumerable.Range(0, 33).Select(_ => new UIHoverImage(NullTexture.Value))];

        public Vector2 activeTilePosition;

        private UIHoverImageButton addDifficultyButton;         private UIHoverImageButton subtractDifficultyButton;         private readonly UIHoverImage tipImage = new(NullTexture.Value);          private bool isInitialized = false;         private int currentWorldID = 0;         private int currentDifficulty = 0;          public override void OnInitialize()         {             mainMenu.SetRectangle(700, 495, -50);             mainMenu.OverflowHidden = true;             startButton = mainMenu.AddTextButton(SUStartButtonText, 1.1f);

            contentContainer.SetRectangle(30, 0, 180, 510);
            mainMenu.Append(contentContainer);

            scrollbar.SetRectangle(0, 10, 20, 450);
            scrollbar.SetView(450, 510);
            mainMenu.Append(scrollbar);              for (int i = 0; i < worldpanels.Length; i++)
            {
                int yPos = i * 60;
                worldpanels[i].SetRectangle(0, yPos, 180, 50);
                contentContainer.Append(worldpanels[i]);
            }

            for (int i = 0; i < worldtexts.Length; i++)
            {
                worldtexts[i].SetRectangle(0, 5, 180, 50);
                worldpanels[i].Append(worldtexts[i]);
            }

            bossListText.SetRectangle(250, 60, 20, 20);
            mainMenu.Append(bossListText);

            enemyListText.SetRectangle(250, 110, 20, 20);
            mainMenu.Append(enemyListText);

            difficultyLabel.SetRectangle(360, 410, 20, 20);
            difficultyLabel._textScale = 1.2f;
            difficultyLabel.TextColor = new(200, 80, 80);
            mainMenu.Append(difficultyLabel);

            difficultyValue.SetRectangle(370, 440, 20, 20);
            difficultyValue._textScale = 1.2f;
            mainMenu.Append(difficultyValue);

            enemyTitle.SetRectangle(320, 10, 20, 20);
            enemyTitle._textScale = 1.3f;
            enemyTitle.TextColor = new(255, 110, 180);
            mainMenu.Append(enemyTitle);

            rewardTitle.SetRectangle(320, 195, 20, 20);
            rewardTitle._textScale = 1.3f;
            rewardTitle.TextColor = new(255, 110, 180);
            mainMenu.Append(rewardTitle);

            for (int i = 0; i < bossImages.Length; i++)
            {
                bossImages[i].SetRectangle(305 + i * 50, 50, 20, 20);
                mainMenu.Append(bossImages[i]);
            }

            for (int i = 0; i < enemyImages.Length; i++)
            {
                enemyImages[i].SetRectangle(325 + (i % 8) * 40, 100 + i / 8 * 40, 20, 20);
                mainMenu.Append(enemyImages[i]);
            }

            for (int i = 0; i < awardImages.Length; i++)
            {
                int col = i % 11;

                int offset = col / 2 + 1;
                int finalCol = (col % 2 == 0) ? 5 - offset : 5 + offset;

                if (finalCol < 0 || finalCol >= 11)
                {
                    finalCol = col;
                }

                awardImages[i].SetRectangle(250 + finalCol * 40, 235 + i / 11 * 40, 20, 20);
                mainMenu.Append(awardImages[i]);
            }

            addDifficultyButton = new(AddDifficultyButtonTexture, AddDifficultyButton_HoverTexture);
            addDifficultyButton.SetRectangle(400, 440, 26, 26);
            addDifficultyButton.OnLeftClickButton += AddDifficultyButtonClicked;
            mainMenu.Append(addDifficultyButton);

            subtractDifficultyButton = new(SubtractDifficultyButtonTexture, SubtractDifficultyButton_HoverTexture);
            subtractDifficultyButton.SetRectangle(340, 440, 26, 26);
            subtractDifficultyButton.OnLeftClickButton += SubtractDifficultyButtonClicked;
            mainMenu.Append(subtractDifficultyButton);

            tipImage.SetRectangle(650, 0, 26, 26);
            mainMenu.Append(tipImage);

            Append(mainMenu);         }

        private void AddDifficultyButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentDifficulty < 7)
            {
                currentDifficulty++;
            }
            difficultyValue.SetText(IntToRoman(currentDifficulty + 1));
        }

        private void SubtractDifficultyButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (currentDifficulty > 0)
            {
                currentDifficulty--;
            }
            difficultyValue.SetText(IntToRoman(currentDifficulty + 1));
        }          public override void Update(GameTime gameTime)         {             mainMenu.CheckMouseInUI();             worldtexts[8].EnforceUIElementInteraction(mainMenu);

            for (int i = 0; i < worldtexts.Length; i++)
            {
                worldtexts[i].SetRectangle(0, 5, 180, 50);
            }              startButton.Top.Set(450, 0);             startButton.Left.Set(520, 0);

            float offsetY = -scrollbar.ViewPosition;
            contentContainer.Top.Set(offsetY, 0);

            WorldDifficultyData worldDifficultyData = WorldDifficultyData.BaseData[currentWorldID][currentDifficulty];
            List<int> bossIds = worldDifficultyData.BossIDs;
            for (int i = 0; i < bossIds.Count; i++)
            {
                bossImages[i].ScaleNPCImage(bossIds[i]);
            }

            List<int> enemyIds = [.. worldDifficultyData.EnemyIDs, .. worldDifficultyData.EliteIDs];
            HashSet<int> uniqueDisplayIds = [];
            foreach (int id in enemyIds)
            {
                uniqueDisplayIds.Add(EnemyDisplayMapping.GetDisplayID(id));
            }
            List<int> displayIds = [.. uniqueDisplayIds.Take(16)];
            for (int i = 0; i < Math.Min(displayIds.Count, enemyImages.Length); i++)
            {
                enemyImages[i].ScaleNPCImage(displayIds[i]);
            }
            if (enemyImages.Length > enemyIds.Count)
            {
                for (int i = enemyIds.Count; i < enemyImages.Length; i++)
                {
                    enemyImages[i].Set(NullTexture.Value);
                }
            }

            List<int> awardIds = worldDifficultyData.AwardIDs;
            for (int i = 0; i < awardIds.Count; i++)
            {
                awardImages[i].ScaleItemImage(awardIds[i]);
            }
            if (awardImages.Length > awardIds.Count)
            {
                for (int i = awardIds.Count; i < awardImages.Length; i++)
                {
                    awardImages[i].Set(NullTexture.Value);
                }
            }

            tipImage.Set(TipsButtonTexture.Value, TipsButton_HoverTexture.Value, SUTipsText, true);
        }          public void InitializeUI()         {             if (!isInitialized)             {                 startButton.AddMouseEvent((UIMouseEvent evt, UIElement listeningElement) =>                 {                     GetInstance<SUEvent>().StartEvent(currentWorldID, currentDifficulty, activeTilePosition);                 });

                for (int i = 0; i < worldtexts.Length; i++)
                {
                    int j = i;
                    worldtexts[j].AddMouseEvent((UIMouseEvent evt, UIElement listeningElement) =>
                    {
                        currentWorldID = j;
                    });
                    worldtexts[j].SetText(SUWorldText[j]);
                }

                startButton.SetText(SUStartButtonText);
                bossListText.SetText($"Boss:");
                enemyListText.SetText($"{SUEnemyListText}:");
                enemyTitle.SetText(SUEnemyTitalText);
                rewardTitle.SetText(SURewardTitalText);
                difficultyLabel.SetText(SUDifficultyText);
                difficultyValue.SetText(IntToRoman(currentDifficulty + 1));                  isInitialized = true;             }         }          public static class EnemyDisplayMapping
        {
            public static readonly Dictionary<int, int> DisplayMapping = new()
            {
                // 基础僵尸变体
                [NPCID.SmallZombie] = NPCID.Zombie,
                [NPCID.BigZombie] = NPCID.Zombie,
                [NPCID.ArmedZombie] = NPCID.Zombie,

                // 光头僵尸变体
                [NPCID.BaldZombie] = NPCID.Zombie,
                [NPCID.SmallBaldZombie] = NPCID.Zombie,
                [NPCID.BigBaldZombie] = NPCID.Zombie,

                // 针垫僵尸变体
                [NPCID.PincushionZombie] = NPCID.Zombie,
                [NPCID.SmallPincushionZombie] = NPCID.Zombie,
                [NPCID.BigPincushionZombie] = NPCID.Zombie,
                [NPCID.ArmedZombiePincussion] = NPCID.Zombie,

                // 史莱姆僵尸变体
                [NPCID.SlimedZombie] = NPCID.Zombie,
                [NPCID.SmallSlimedZombie] = NPCID.Zombie,
                [NPCID.BigSlimedZombie] = NPCID.Zombie,
                [NPCID.ArmedZombieSlimed] = NPCID.Zombie,

                // 沼泽僵尸变体
                [NPCID.SwampZombie] = NPCID.Zombie,
                [NPCID.SmallSwampZombie] = NPCID.Zombie,
                [NPCID.BigSwampZombie] = NPCID.Zombie,
                [NPCID.ArmedZombieSwamp] = NPCID.Zombie,

                // 丛林僵尸变体
                [NPCID.TwiggyZombie] = NPCID.Zombie,
                [NPCID.SmallTwiggyZombie] = NPCID.Zombie,
                [NPCID.BigTwiggyZombie] = NPCID.Zombie,
                [NPCID.ArmedZombieTwiggy] = NPCID.Zombie,

                // 女性僵尸变体
                [NPCID.FemaleZombie] = NPCID.Zombie,
                [NPCID.SmallFemaleZombie] = NPCID.Zombie,
                [NPCID.BigFemaleZombie] = NPCID.Zombie,
                [NPCID.ArmedZombieCenx] = NPCID.Zombie,

                // 火炬僵尸变体
                [NPCID.TorchZombie] = NPCID.Zombie,
                [NPCID.ArmedTorchZombie] = NPCID.Zombie,

                // 雨衣僵尸变体
                [NPCID.ZombieRaincoat] = NPCID.Zombie,
                [NPCID.SmallRainZombie] = NPCID.Zombie,
                [NPCID.BigRainZombie] = NPCID.Zombie,

                // 爱斯基摩僵尸变体
                [NPCID.ZombieEskimo] = NPCID.Zombie,
                [NPCID.ArmedZombieEskimo] = NPCID.Zombie,

                // 恶魔眼变体
                [NPCID.DemonEye2] = NPCID.DemonEye,
                [NPCID.CataractEye] = NPCID.DemonEye,
                [NPCID.CataractEye2] = NPCID.DemonEye,
                [NPCID.SleepyEye] = NPCID.DemonEye,
                [NPCID.SleepyEye2] = NPCID.DemonEye,
                [NPCID.DialatedEye] = NPCID.DemonEye,
                [NPCID.DialatedEye2] = NPCID.DemonEye,
                [NPCID.GreenEye] = NPCID.DemonEye,
                [NPCID.GreenEye2] = NPCID.DemonEye,
                [NPCID.PurpleEye] = NPCID.DemonEye,
                [NPCID.PurpleEye2] = NPCID.DemonEye,
            };

            // 获取显示用的NPCID（如果没有映射则返回原ID）
            public static int GetDisplayID(int originalID)
            {
                return DisplayMapping.TryGetValue(originalID, out int displayID) ? displayID : originalID;
            }
        }     } } 