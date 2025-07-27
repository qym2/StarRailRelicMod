namespace StarRailRelic.Content.Pets.Meme
{
    public class QuillOfRemembrance : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToVanitypet(ProjectileType<MemeProjectile>(), BuffType<MemeBuff>());

            Item.width = 44;
            Item.height = 70;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 5);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            return false;
        }
    }
}
