namespace StarRailRelic.Content.Projectiles.Hostile
{
    public class CorrosiveBarrage : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;

            Projectile.timeLeft = 600;
            Projectile.penetrate = 3;
            Projectile.alpha = 55;

            Projectile.friendly = false;
            Projectile.hostile = true;

            Projectile.tileCollide = true;

            Projectile.netImportant = true;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void AI()
        {
            Projectile.RotateByVelocity(isInclined: false);

            for (int i = 0; i < 3; i++)
            {
                if (Projectile.direction == 1)
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 10, Projectile.Center.Y - 12), 20, 10, DustID.RedMoss, -4, 0, 125);
                    dust.noGravity = true;
                }
                if (Projectile.direction == -1)
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X, Projectile.Center.Y - 12), 20, 10, DustID.RedMoss, 4, 0, 125);
                    dust.noGravity = true;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Venom, 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}
