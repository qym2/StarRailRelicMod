namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class WeaponChargingComponent : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 4, 0, 0);

            Item.rare = RarityType<PurpleRarity>();

            Item.width = 39;
            Item.height = 41;
        }
    }
}
