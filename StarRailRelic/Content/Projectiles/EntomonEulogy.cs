namespace StarRailRelic.Content.Projectiles
{
    public class EntomonEulogy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.timeLeft = 600;

            Projectile.width = 52;
            Projectile.height = 52;
            Projectile.alpha = 255;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            int frameSpeed = 5;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= 6)
                {
                    Projectile.Kill();
                }
            }

            Projectile.ai[1] += 0.07f;
            Projectile.scale = Projectile.ai[1];

            Projectile.alpha -= 63;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffType<ShatterDebuffI>(), 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffType<ShatterDebuffI>(), 300);
        }
    }
}
