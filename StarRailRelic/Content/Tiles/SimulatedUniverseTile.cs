namespace StarRailRelic.Content.Tiles
{
    public class SimulatedUniverseTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 2000;
            Main.tileNoAttach[Type] = true;
            Main.tileBlockLight[Type] = false;

            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.AvoidedByNPCs[Type] = true;

            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(2, 4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [
                TileID.MagicalIceBlock,
                TileID.Boulder,
                TileID.BouncyBoulder,
                TileID.LifeCrystalBoulder,
                TileID.RollingCactus
            ];
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 172, 226), CreateMapEntryName());

            DustType = DustID.GemDiamond;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }

        public override bool Slope(int i, int j)
        {
            return false;
        }

        public override bool RightClick(int i, int j)
        {
            if (!GetInstance<SUEvent>().Ongoing)
            {
                GetInstance<SUMainUISystem>().ActiveTilePosition = new Vector2(i, j) * 16;
                GetInstance<SUMainUISystem>().ShowUI();
                return true;
            }
            return base.RightClick(i, j);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (GetInstance<SUEvent>().Ongoing)
            {
                return false;
            }
            return base.CanKillTile(i, j, ref blockDamaged);
        }

        public override bool CanExplode(int i, int j)
        {
            if (GetInstance<SUEvent>().Ongoing)
            {
                return false;
            }
            return base.CanExplode(i, j);
        }

        public override bool CanPlace(int i, int j)
        {
            if (GetInstance<SUEvent>().Ongoing)
            {
                return false;
            }
            return base.CanPlace(i, j);
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced)
        {
            if (GetInstance<SUEvent>().Ongoing)
            {
                return false;
            }
            return base.CanReplace(i, j, tileTypeBeingPlaced);
        }
    }

    public class SUGlobalTile : GlobalTile
    {
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            Point16 posTopLeft = GetInstance<SUEvent>().posTopLeft;
            Point16 posBottomRight = GetInstance<SUEvent>().posBottomRight;

            bool isInRange = i >= posTopLeft.X && i <= posBottomRight.X &&
                 j >= posTopLeft.Y && j <= posBottomRight.Y;

            if (GetInstance<SUEvent>().Ongoing && isInRange)
            {
                return false;
            }
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanExplode(int i, int j, int type)
        {
            Point16 posTopLeft = GetInstance<SUEvent>().posTopLeft;
            Point16 posBottomRight = GetInstance<SUEvent>().posBottomRight;

            bool isInRange = i >= posTopLeft.X && i <= posBottomRight.X &&
                 j >= posTopLeft.Y && j <= posBottomRight.Y;

            if (GetInstance<SUEvent>().Ongoing && isInRange)
            {
                return false;
            }
            return base.CanExplode(i, j, type);
        }

        public override bool CanPlace(int i, int j, int type)
        {
            Point16 posTopLeft = GetInstance<SUEvent>().posTopLeft;
            Point16 posBottomRight = GetInstance<SUEvent>().posBottomRight;

            bool isInRange = i >= posTopLeft.X && i <= posBottomRight.X &&
                 j >= posTopLeft.Y && j <= posBottomRight.Y;

            if (GetInstance<SUEvent>().Ongoing && isInRange)
            {
                return false;
            }
            return base.CanPlace(i, j, type);
        }

        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            Point16 posTopLeft = GetInstance<SUEvent>().posTopLeft;
            Point16 posBottomRight = GetInstance<SUEvent>().posBottomRight;

            bool isInRange = i >= posTopLeft.X && i <= posBottomRight.X &&
                 j >= posTopLeft.Y && j <= posBottomRight.Y;

            if (GetInstance<SUEvent>().Ongoing && isInRange)
            {
                return false;
            }
            return base.CanReplace(i, j, type, tileTypeBeingPlaced);
        }
    }
}