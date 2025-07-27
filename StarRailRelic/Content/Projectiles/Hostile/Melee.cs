namespace FusionMod.Content.Projectiles.Hostile
{
    public class Melee : ModProjectile
    {
        public override string Texture => NullTexturePath;

        private IEntitySource entitySource;

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;

            Projectile.timeLeft = 20;
            Projectile.penetrate = -1;

            Projectile.friendly = false;
            Projectile.hostile = true;

            Projectile.tileCollide = false;

            Projectile.netImportant = true;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void OnSpawn(IEntitySource source)
        {
            entitySource = source;
        }

        public override void AI()
        {
            Projectile.velocity *= 0;

            /*
            if (entitySource is EntitySource_OwnerNPC ownerNPC && ownerNPC.OwnerNPC.ModNPC is UndeadExecutioner executioner)
            {
                Projectile.Center = ownerNPC.OwnerNPC.Center + new Vector2(executioner.MeleeOffsetX, -15);
            }*/

            Projectile.netUpdate = true;
        }
    }
}
