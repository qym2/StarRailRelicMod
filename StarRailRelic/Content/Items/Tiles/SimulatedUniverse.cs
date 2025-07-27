namespace StarRailRelic.Content.Items.Tiles
{
    public class SimulatedUniverse : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(TileType<SimulatedUniverseTile>());

            Item.width = 84;
            Item.height = 78;

            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<GoldRarity>();
        }
    }
}
