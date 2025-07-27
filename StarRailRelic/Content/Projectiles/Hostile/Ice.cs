namespace StarRailRelic.Content.Projectiles.Hostile
{
    public class Ice : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 70;

            Projectile.timeLeft = 600;
            Projectile.penetrate = 3;
            Projectile.alpha = 55;
            Projectile.scale = 0.8f;

            Projectile.friendly = false;
            Projectile.hostile = true;

            Projectile.tileCollide = true;

            Projectile.netImportant = true;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void AI()
        {
            Projectile.RotateByVelocity(isInclined: false);

            if (Main.rand.NextBool(3)) // 控制生成频率（每3帧生成1次）
            {
                // 生成位置在弹幕后方偏移
                Vector2 trailPos = Projectile.Center - Projectile.velocity * 0.5f;

                Dust iceTrail = Dust.NewDustPerfect(
                    trailPos,
                    DustID.Ice,                  // 使用原版冰晶粒子
                    Velocity: Projectile.velocity * -0.2f + Main.rand.NextVector2Circular(0.5f, 0.5f),
                    Scale: Main.rand.NextFloat(0.8f, 1.5f)
                );

                iceTrail.noGravity = true;
                iceTrail.alpha = 150;            // 半透明效果
                iceTrail.color = new Color(180, 220, 255, 100); // 淡蓝色调
            }
        }

        public override void OnKill(int timeLeft)
        {
            // 基础冰块碎片
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Ice, // 原版冰晶粒子
                    Main.rand.NextVector2CircularEdge(8f, 8f), // 向外扩散
                    Scale: Main.rand.NextFloat(0.8f, 1.5f),
                    newColor: new Color(180, 220, 255, 100)
                ).noGravity = true;
            }

            // 冰雾效果
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Frost, // 原版霜冻粒子
                    SpeedX: Main.rand.NextFloat(-6f, 6f),
                    SpeedY: Main.rand.NextFloat(-4f, 2f),
                    Scale: Main.rand.NextFloat(1f, 1.8f)
                ).noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (target.HasBuff(BuffID.OnFire))
            {
                target.ClearBuff(BuffID.OnFire);
                modifiers.FinalDamage *= 1.5f;
            }
            if (target.HasBuff(BuffID.Chilled))
            {
                target.AddBuff(BuffID.Frozen, 60);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Chilled, 300);
        }
    }
}
