﻿namespace StarRailRelic.Content.Items.Miscellaneous
{
    public class LostCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;

            Item.value = Item.sellPrice(0, 0, 0, 12);

            Item.rare = RarityType<PurpleRarity>();

            Item.width = 33;
            Item.height = 30;
        }
    }
}
