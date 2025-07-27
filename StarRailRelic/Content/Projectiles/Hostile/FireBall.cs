namespace StarRailRelic.Content.Projectiles.Hostile
{
    public class FireBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 15;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 600;

            Projectile.width = 90;
            Projectile.height = 90;

            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void AI()
        {
            Projectile.RotateByVelocity(isInclined: false);
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.6f, 0.2f));

            int frameSpeed = 5;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= 15)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item45, Projectile.Center);
            CreateVanillaFireExplosion(Projectile.Center, 1.5f);
            CreateLavaEruption(Projectile.Center);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (target.HasBuff(BuffID.Chilled))
            {
                target.ClearBuff(BuffID.Chilled);
                modifiers.FinalDamage *= 2f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}
