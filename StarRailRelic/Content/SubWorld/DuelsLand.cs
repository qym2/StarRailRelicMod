namespace StarRailRelic.Content.SubWorld
{
    public class DuelsLand : Subworld
    {
        public override int Width => 5000;
        public override int Height => 2100;

        public override bool NormalUpdates => false;
        public override List<GenPass> Tasks => [];

        public override void OnLoad()
        {
        }

        public override void OnEnter()
        {
            SubworldSystem.noReturn = true;
            Main.LocalPlayer.GetModPlayer<SubworldPlayer>().InSubworld = true;
        }

        public override void OnExit()
        {
            Main.LocalPlayer.GetModPlayer<SubworldPlayer>().InSubworld = false;
        }
    }

    public class ExampleGenPass : GenPass
    {
        public ExampleGenPass() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating terrain";
            Main.worldSurface = Main.maxTilesY - 42;
            Main.rockLayer = Main.maxTilesY;
            //for (int i = 0; i < Main.maxTilesX; i++)
            //{
            //    for (int j = 0; j < Main.maxTilesY; j++)
            //    {
            //        progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY));

            //        if (j % 12 == 0)
            //        {
            //            Tile tile = Main.tile[i, j];
            //            tile.HasTile = true;
            //            tile.TileType = TileID.Dirt;
            //        }
            //    }
            //}
        }
    }

    public class UpdateSubworldSystem : ModSystem
    {
        public override void PreUpdateWorld()
        {
            if (SubworldSystem.IsActive<DuelsLand>())
            {
                // 更新机制
                Wiring.UpdateMech();

                // 更新瓦片实体
                TileEntity.UpdateStart();
                foreach (TileEntity te in TileEntity.ByID.Values)
                {
                    te.Update();
                }
                TileEntity.UpdateEnd();

                // 更新液体
                if (++Liquid.skipCount > 1)
                {
                    Liquid.UpdateLiquid();
                    Liquid.skipCount = 0;
                }
            }
        }

        //public static bool JustPressed(Keys key)
        //{
        //    return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
        //}

        //public override void PostUpdateWorld()
        //{
        //    if (JustPressed(Keys.O))
        //    {
        //        for (int x = 0; x < Main.maxTilesX; x++)
        //        {
        //            for (int y = 0; y < Main.maxTilesY; y++)
        //            {
        //                Tile tile = Main.tile[x, y];

        //                // 将黑檀石块变为土块
        //                if (tile.TileType == TileID.Ash)
        //                {
        //                    tile.ResetToType(TileID.Hellstone);
        //                }
        //                // 将乌木栅栏变为木栅栏
        //                else if (tile.WallType == WallID.Marble)
        //                {
        //                    tile.WallType = WallID.HellstoneBrick;
        //                }
        //                // 将损伤块变为木材
        //                else if (tile.TileType == TileID.MarbleBlock)
        //                {
        //                    tile.ResetToType(TileID.HellstoneBrick);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public class SubworldGlobalTile : GlobalTile
    {
        //public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        //{
        //    SubworldPlayer modPlayer = Main.LocalPlayer.GetModPlayer<SubworldPlayer>();

        //    if (modPlayer.InSubworld)
        //    {
        //        Vector2 playerPosition = Main.LocalPlayer.position;
        //        Vector2 tilePosition = new(i * 16, j * 16);

        //        float distance = Vector2.Distance(playerPosition, tilePosition);

        //        float distanceThreshold = 10000f;

        //        if (distance < distanceThreshold)
        //        {
        //            return false;
        //        }
        //    }
        //    return base.CanKillTile(i, j, type, ref blockDamaged);
        //}

        //public override bool CanExplode(int i, int j, int type)
        //{
        //    SubworldPlayer modPlayer = Main.LocalPlayer.GetModPlayer<SubworldPlayer>();

        //    if (modPlayer.InSubworld)
        //    {
        //        Vector2 playerPosition = Main.LocalPlayer.position;
        //        Vector2 tilePosition = new(i * 16, j * 16);

        //        float distance = Vector2.Distance(playerPosition, tilePosition);

        //        float distanceThreshold = 10000f;

        //        if (distance < distanceThreshold)
        //        {
        //            return false;
        //        }
        //    }
        //    return base.CanExplode(i, j, type);
        //}

        //public override bool CanPlace(int i, int j, int type)
        //{
        //    SubworldPlayer modPlayer = Main.LocalPlayer.GetModPlayer<SubworldPlayer>();

        //    if (modPlayer.InSubworld)
        //    {
        //        Vector2 playerPosition = Main.LocalPlayer.position;
        //        Vector2 tilePosition = new(i * 16, j * 16);

        //        float distance = Vector2.Distance(playerPosition, tilePosition);

        //        float distanceThreshold = 10000f;

        //        if (distance < distanceThreshold)
        //        {
        //            return false;
        //        }
        //    }
        //    return base.CanPlace(i, j, type);
        //}

        //public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        //{
        //    SubworldPlayer modPlayer = Main.LocalPlayer.GetModPlayer<SubworldPlayer>();

        //    if (modPlayer.InSubworld)
        //    {
        //        Vector2 playerPosition = Main.LocalPlayer.position;
        //        Vector2 tilePosition = new(i * 16, j * 16);

        //        float distance = Vector2.Distance(playerPosition, tilePosition);

        //        float distanceThreshold = 10000f;

        //        if (distance < distanceThreshold)
        //        {
        //            return false;
        //        }
        //    }
        //    return base.CanReplace(i, j, type, tileTypeBeingPlaced);
        //}
    }
}
