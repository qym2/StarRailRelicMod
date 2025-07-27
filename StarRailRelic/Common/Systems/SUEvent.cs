using Terraria;

namespace StarRailRelic.Common.Systems
{
    #region 枚举
    /// <summary>
    /// 波次类型枚举
    /// </summary>
    public enum WaveType
    {
        /// <summary>
        /// 战斗
        /// </summary>
        Combat,
        /// <summary>
        /// 事件
        /// </summary>
        Event,      
        /// <summary>
        /// 遭遇
        /// </summary>
        Encounter,  
        /// <summary>
        /// 冒险
        /// </summary>
        Adventure,  
        /// <summary>
        /// 奇遇
        /// </summary>
        Special,    
        /// <summary>
        /// 商店
        /// </summary>
        Shop,       
        /// <summary>
        /// 休整
        /// </summary>
        Rest,       
        /// <summary>
        /// 精英
        /// </summary>
        Elite,      
        /// <summary>
        /// 首领
        /// </summary>
        Boss        
    }
    #endregion

    public class WorldDifficultyData(List<int> enemyIDs, List<int> eliteIDs, List<int> bossIDs, List<int> awardIDs)
    {
        public List<int> EnemyIDs { get; set; } = enemyIDs;
        public List<int> EliteIDs { get; set; } = eliteIDs;
        public List<int> BossIDs { get; set; } = bossIDs;
        public List<int> AwardIDs { get; set; } = awardIDs;

        public static Dictionary<int, Dictionary<int, WorldDifficultyData>> BaseData =>
        new()
        {
            [0] = new()
            {
                [0] = new WorldDifficultyData(
                    CreateNormalZombiesMixList(CreateDemonEyesMixList()), 
                    [NPCID.ZombieElfGirl, NPCID.ZombieDoctor], 
                    [NPCID.KingSlime, NPCID.EyeofCthulhu],
                    [ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel, ItemID.HealingPotion, ItemID.Gel]),
                [1] = new WorldDifficultyData(
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.KingSlime, NPCID.EyeofCthulhu],
                    [ItemID.Gel, ItemID.Gel]),
            },

            [1] = new Dictionary<int, WorldDifficultyData>
            {
                [0] = new WorldDifficultyData(
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.Zombie, NPCID.EyeofCthulhu],
                    [ItemID.Lens]),
                [1] = new WorldDifficultyData(
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.Zombie, NPCID.EyeofCthulhu], 
                    [NPCID.Zombie, NPCID.EyeofCthulhu],
                    [ItemID.Gel, ItemID.Gel]),
            }
        };

        public static List<int> CreateNormalZombiesMixList(params int[] enemies)
        {
            return [.. SUNPC.AllNormalZombies, .. enemies];
        }

        public static List<int> CreateNormalZombiesMixList(List<int> enemies)
        {
            return [.. SUNPC.AllNormalZombies, .. enemies];
        }

        public static List<int> CreateDemonEyesMixList(params int[] enemies)
        {
            return [.. SUNPC.AllDemonEyes, .. enemies];
        }

        public static List<int> CreateDemonEyesMixList(List<int> enemies)
        {
            return [.. SUNPC.AllDemonEyes, .. enemies];
        }
    }

    public class SUEvent : ModSystem
    {
        // 全局状态
        public bool Ongoing;
        public Dictionary<WaveType, int> WaveVotes = [];
        public int VoteTimer;
        public Point16 posTopLeft;
        public Point16 posBottomRight;

        private int worldID;
        private int difficulty;
        private int currentWave;
        private WaveType currentWaveType;
        private List<WaveType> waveOptions = [];
        private double originalTime;
        private bool originalDayTime;
        private Vector2 activeTilePosition;
        private readonly int checkTileRadius = 75;

        // 战斗波次的价值相关变量
        private float totalCombatValue; // 本波次总价值
        private float remainingCombatValue; // 剩余可生成的价值
        private int spawnInterval = 40; // 生成间隔（帧数，60帧=1秒）
        private int spawnTimer; // 生成计时器（累加至spawnInterval时触发生成）
        private int activeEnemies; // 当前存活的怪物数量（用于判定战斗结束）

        public override void OnWorldLoad()
        {
            Ongoing = false;
            currentWave = -1;
            worldID = -1;
            difficulty = -1; 
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="worldID">世界ID</param>
        /// <param name="difficulty">难度等级</param>
        /// <param name="activeTilePosition">图格原点</param>
        public void StartEvent(int worldID, int difficulty, Vector2 activeTilePosition)
        {
            this.activeTilePosition = activeTilePosition;

            if (!CheckTile())
            {
                ChatHelper.BroadcastChatMessage(SUCheckTileFiledText.ToNetworkText(), Color.Red);
                return;
            }

            GetInstance<SUMainUISystem>().HideUI();
            Ongoing = true;
            currentWave = 0;
            this.worldID = worldID;
            this.difficulty = difficulty;

            originalTime = Main.time;
            originalDayTime = Main.dayTime;

            Main.time = 0;
            Main.dayTime = false;

            // 重置所有玩家投票状态
            foreach (Player player in Main.ActivePlayers)
            {
                SUPlayer suPlayer = player.GetModPlayer<SUPlayer>();
                suPlayer.VoteChoice = null;
            }

            GenerateArena();

            StartNextWave();
        }

        public void EndEvent()
        {
            Ongoing = false;

            ClearTile(); 
            
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.GetGlobalNPC<SUNPC>().IsSimulatedEnemy)
                {
                    npc.active = false;
                }
            }

            Main.time = originalTime;
            Main.dayTime = originalDayTime;
        }

        private void ClearTile()
        {
            for (int x = posTopLeft.X; x <= posBottomRight.X; x++)
            {
                for (int y = posTopLeft.Y; y <= posBottomRight.Y; y++)
                {
                    // 边界检查
                    if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
                    {
                        return;
                    }

                    Tile tile = Main.tile[x, y];

                    // 检查是否有物块或墙壁
                    if (tile.HasTile || tile.WallType > 0)
                    {
                        // 允许装置自身所在的物块
                        if (tile.type == TileType<SimulatedUniverseTile>()
                            || Main.tile[x, y - 1].TileType == TileType<SimulatedUniverseTile>())
                        {
                            continue;
                        }
                        
                        tile.ClearEverything();
                    }
                }
            }
        }

        private bool CheckTile()
        {
            posTopLeft = new((int)(activeTilePosition.X / 16 - checkTileRadius), 
                (int)(activeTilePosition.Y / 16 - checkTileRadius));
            posBottomRight = new((int)(activeTilePosition.X / 16 + checkTileRadius),
                (int)(activeTilePosition.Y / 16 + checkTileRadius));

            for (int x = posTopLeft.X; x <= posBottomRight.X; x++)
            {
                for (int y = posTopLeft.Y; y <= posBottomRight.Y; y++)
                {
                    // 边界检查
                    if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
                    {
                        return false;
                    }

                    Tile tile = Main.tile[x, y];

                    // 检查是否有物块或墙壁
                    if (tile.HasTile || tile.WallType > 0)
                    {
                        // 允许装置自身所在的物块
                        if (tile.type == TileType<SimulatedUniverseTile>()
                            || Main.tile[x, y - 1].TileType == TileType<SimulatedUniverseTile>())
                        {
                            continue;
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        public void GenerateArena()
        {
            Generator.GenerateStructure("Assets/Structures/SUArenaWorld1Difficulty1", posTopLeft, Mod);
        }

        // 开始下一波
        public void StartNextWave()
        {
            currentWave++;

            // 重置所有玩家投票
            //ResetAllVotes();
            
            // 固定波次类型
            if (currentWave == 1)
            {
                currentWaveType = WaveType.Combat;
                ExecuteWave(currentWaveType);
            }
            else if (currentWave == 4 || currentWave == 8)
            {
                currentWaveType = WaveType.Elite;
                ExecuteWave(currentWaveType);
            }
            else
            {
                currentWaveType = WaveType.Combat;
                ExecuteWave(currentWaveType);
                // 生成随机波次选项
                //GenerateWaveOptions();

                // 开始投票计时
                //VoteTimer = 30; // 30秒投票时间
            }

            Main.NewText($"当前波次:{currentWave}，波次类型{currentWaveType}");

            /*
            else if (currentWave == 12)
            {
                currentWaveType = WaveType.Rest;
                ExecuteWave(currentWaveType);
            }
            else if (currentWave == 13)
            {
                currentWaveType = WaveType.Boss;
                ExecuteWave(currentWaveType);
            }
            */
        }

        // 执行波次
        private void ExecuteWave(WaveType waveType)
        {
            currentWaveType = waveType;

            switch (waveType)
            {
                case WaveType.Combat:
                    SpawnCombatWave();
                    break;
                case WaveType.Event:
                    break;
                case WaveType.Encounter:
                    break;
                case WaveType.Adventure:
                    break;
                case WaveType.Special:
                    break;
                case WaveType.Shop:
                    break;
                case WaveType.Rest:
                    break;
                case WaveType.Elite:
                    SpawnCombatWave(true);
                    break;
                case WaveType.Boss:
                    break;
                default:
                    break;
            }
        }

        #region 战斗
        /// <summary>
        /// 基于怪物价值的战斗波次生成逻辑
        /// </summary>
        private void SpawnCombatWave(bool elite = false)
        {
            // 1. 计算本波次的总价值（随波次和难度递增）
            // 公式：基础价值 + 波次加成 + 难度加成（可根据平衡调整）
            totalCombatValue = (1 + difficulty * 0.5f) * (2000 + (currentWave * 300));
            if (elite)
            {
                totalCombatValue *= 2;
            }

            // 2. 初始化变量
            remainingCombatValue = totalCombatValue;
            spawnTimer = 0;
            activeEnemies = 0;

            // 3. 显示波次信息（告知玩家本波总价值）
            Main.NewText($"第{currentWave}波 - 总价值: {totalCombatValue}", Color.LightGreen);

            // 4. 首次生成一只怪物（启动战斗）
            TrySpawnEnemy(elite);
        }

        /// <summary>
        /// 尝试生成一只怪物（检查剩余价值并随机选择）
        /// </summary>
        private void TrySpawnEnemy(bool elite)
        {
            if (remainingCombatValue <= 0)
            {
                return; // 剩余价值为0，停止生成
            }

            WorldDifficultyData data = WorldDifficultyData.BaseData[worldID][difficulty];
            // 获取当前波次的敌人池
            List<int> enemyPool = elite ? data.EliteIDs : data.EnemyIDs;
            if (enemyPool.Count == 0)
            {
                return;
            }

            // 随机选择一个怪物类型
            int randomEnemyType = Main.rand.Next(enemyPool);

            // 获取该怪物的基础价值（使用原版NPC的value属性）
            NPC tempNPC = new();
            tempNPC.SetDefaults(randomEnemyType);
            float enemyValue = tempNPC.value;

            // 检查剩余价值是否足够生成该怪物
            if (enemyValue <= remainingCombatValue)
            {
                // 生成怪物
                Vector2 spawnPos = GetRandomSpawnPosition();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int npcIndex = NPC.NewNPC(Entity.GetSource_None(),
                        (int)spawnPos.X, (int)spawnPos.Y, randomEnemyType);

                    // 调整怪物属性（随难度增强）
                    NPC npc = Main.npc[npcIndex];
                    npc.value = 0;
                    npc.npcSlots = 0;
                    npc.SpawnedFromStatue = true;
                    npc.lifeMax = (int)(npc.lifeMax * (1f + (currentWave * 0.15f) + (difficulty * 0.3f)));
                    npc.life = npc.lifeMax;
                    npc.damage = (int)(npc.damage * (1f + (currentWave * 0.1f) + (difficulty * 0.2f)));

                    // 标记为当前波次的怪物（方便后续跟踪）
                    npc.GetGlobalNPC<SUNPC>().IsCurrentWaveEnemy = true;
                    npc.GetGlobalNPC<SUNPC>().IsSimulatedEnemy = true;

                    // 同步到服务器
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
                    }

                    // 更新剩余价值和存活怪物数量
                    remainingCombatValue -= enemyValue;
                    activeEnemies++;

                    // 显示生成信息（可选）
                    Main.NewText($"生成 {Lang.GetNPCNameValue(randomEnemyType)}，剩余价值: {remainingCombatValue:F1}", Color.Cyan);
                }
            }
            else
            {
                remainingCombatValue = 0;
            }
        }

        /// <summary>
        /// 在指定矩形范围内生成随机位置（预留边缘空间，避免怪物超出）
        /// </summary>
        /// <returns>矩形内的随机像素坐标</returns>
        private Vector2 GetRandomSpawnPosition(int edgePadding = 50)
        {
            // 1. 计算矩形的有效范围（减去边缘预留空间）
            float minX = (posTopLeft.X + edgePadding) * 16;
            float maxX = (posBottomRight.X - edgePadding) * 16;
            float minY = (posTopLeft.Y + edgePadding) * 16;
            float maxY = (posBottomRight.Y - edgePadding) * 16;

            // 3. 在有效范围内随机生成X和Y坐标
            float randomX = Main.rand.NextFloat(minX, maxX);
            float randomY = Main.rand.NextFloat(minY, maxY);

            return new Vector2(randomX, randomY);
        }

        public override void PostUpdateNPCs()
        {
            if (!Ongoing || (currentWaveType != WaveType.Combat && currentWaveType != WaveType.Elite))
            {
                return;
            }

            // 战斗波次中：控制生成间隔
            if (remainingCombatValue > 0)
            {
                spawnTimer++;
                if (spawnTimer >= spawnInterval)
                {
                    TrySpawnEnemy(currentWaveType == WaveType.Elite);
                    spawnTimer = 0; // 重置计时器
                }
            }

            // 检查战斗是否结束（所有生成的怪物被击杀，且无剩余价值）
            CheckCombatWaveEnd();
        }

        /// <summary>
        /// 检查战斗波次是否结束
        /// </summary>
        private void CheckCombatWaveEnd()
        {
            // 重新计算当前存活的怪物数量（仅统计本波次生成的）
            int currentActiveEnemies = 0;

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.GetGlobalNPC<SUNPC>().IsCurrentWaveEnemy)
                {
                    currentActiveEnemies++;
                }
            }
            activeEnemies = currentActiveEnemies;

            // 结束条件：剩余价值为0 且 没有存活的怪物
            if (remainingCombatValue <= 0 && activeEnemies == 0)
            {
                Main.NewText($"第{currentWave}波战斗结束！", Color.Green);
                // 进入下一波准备阶段
                PreStartNextWave();
            }
        }
        #endregion

        /// <summary>
        /// 下一波准备阶段（倒计时）
        /// </summary>
        private void PreStartNextWave()
        {
            spawnTimer = 0;
            StartNextWave();
        }

        // 生成波次选项
        private void GenerateWaveOptions()
        {
            waveOptions.Clear();
            WaveVotes.Clear();

            WeightedRandom<WaveType> weightedRandom = new();
            weightedRandom.Add(WaveType.Combat, 30);
            weightedRandom.Add(WaveType.Event, 20);
            weightedRandom.Add(WaveType.Encounter, 15);
            weightedRandom.Add(WaveType.Adventure, 15);
            weightedRandom.Add(WaveType.Special, 5);
            weightedRandom.Add(WaveType.Shop, 10);

            // 选择3-5个选项
            int optionCount = Main.rand.Next(2, 3);
            for (int i = 0; i < optionCount; i++)
            {
                WaveType selected = weightedRandom.Get();
                waveOptions.Add(selected);
                WaveVotes[selected] = 0;
            }
        }

        #region 多人游戏投票
        // 玩家投票
        public void PlayerVote(Player player, WaveType choice)
        {
            SUPlayer suPlayer = player.GetModPlayer<SUPlayer>();
            suPlayer.VoteChoice = choice;

            // 更新投票计数
            if (WaveVotes.TryGetValue(choice, out int value))
            {
                WaveVotes[choice] = ++value;
            }

            // 检查是否所有玩家都已投票
            CheckVoteCompletion();
        }

        // 检查投票完成
        private void CheckVoteCompletion()
        {
            int activePlayers = Main.player.Count(p => p.active);
            int votedPlayers = Main.player.Count(p => p.active &&
                                    p.GetModPlayer<SUPlayer>().VoteChoice.HasValue);

            if (votedPlayers >= activePlayers)
            {
                EndVoting();
            }
        }

        // 结束投票
        private void EndVoting()
        {
            // 找出票数最高的选项
            currentWaveType = WaveVotes
                .OrderByDescending(kv => kv.Value)
                .First().Key;

            // 执行波次
            ExecuteWave(currentWaveType);
        }
        #endregion

        public override void PostUpdatePlayers()
        {
            if (Ongoing)
            {
                foreach (Player player in Main.ActivePlayers)
                {
                    if (player.Center.X < posTopLeft.X * 16
                        || player.Center.X > posBottomRight.X * 16
                        || player.Center.Y < posTopLeft.Y * 16
                        || player.Center.Y > posBottomRight.Y * 16)
                    {
                        player.Center = activeTilePosition;
                    }
                }
            }
        }

        public override void PreSaveAndQuit()
        {
            if (Ongoing)
            {
                EndEvent();
            }
        }

        public override void OnWorldUnload()
        {
            if (Ongoing)
            {
                EndEvent();
            }
        }

        public override void ClearWorld()
        {
            if (Ongoing)
            {
                EndEvent();
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
        }
    }

    public class SUNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool IsCurrentWaveEnemy { get; set; }
        public bool IsSimulatedEnemy { get; set; }

        // 所有僵尸类型的NPCID集合
        public static readonly List<int> AllNormalZombies =
        [
            NPCID.Zombie, NPCID.SmallZombie, NPCID.BigZombie,
            NPCID.ArmedZombie, NPCID.BaldZombie, NPCID.SmallBaldZombie,
            NPCID.BigBaldZombie, NPCID.PincushionZombie, NPCID.SmallPincushionZombie,
            NPCID.BigPincushionZombie, NPCID.ArmedZombiePincussion,
            NPCID.SlimedZombie, NPCID.SmallSlimedZombie, NPCID.BigSlimedZombie,
            NPCID.ArmedZombieSlimed, NPCID.SwampZombie, NPCID.SmallSwampZombie,
            NPCID.BigSwampZombie, NPCID.ArmedZombieSwamp, NPCID.TwiggyZombie,
            NPCID.SmallTwiggyZombie, NPCID.BigTwiggyZombie, NPCID.ArmedZombieTwiggy,
            NPCID.FemaleZombie, NPCID.SmallFemaleZombie, NPCID.BigFemaleZombie,
            NPCID.ArmedZombieCenx, NPCID.TorchZombie, NPCID.ArmedTorchZombie,
            NPCID.ZombieRaincoat, NPCID.SmallRainZombie, NPCID.BigRainZombie,
            NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo
        ];

        // 所有恶魔眼类型的NPCID集合
        public static readonly List<int> AllDemonEyes =
        [
            NPCID.DemonEye, NPCID.DemonEye2, NPCID.CataractEye,
            NPCID.CataractEye2, NPCID.SleepyEye, NPCID.SleepyEye2,
            NPCID.DialatedEye, NPCID.DialatedEye2, NPCID.GreenEye,
            NPCID.GreenEye2, NPCID.PurpleEye,NPCID.PurpleEye2
        ];
    }
}