//using StarRailRelic.Content.Items.Weapons.Melee;

//namespace StarRailRelic.Common.Projectiles
//{
//    public class BaseballBatReflectProjectile : GlobalProjectile
//    {
//        public override bool InstancePerEntity => true;

//        private bool reflected;

//        public override void AI(Projectile projectile)
//        {
//            Player closestPlayer = null;
//            float closestDistance = float.MaxValue;

//            foreach (Player player in Main.player)
//            {
//                if (player.active && !player.dead)
//                {
//                    float distance = Vector2.Distance(projectile.Center, player.Center);

//                    if (distance < closestDistance)
//                    {
//                        closestDistance = distance;
//                        closestPlayer = player;
//                    }
//                }
//            }

//            if (closestPlayer != null && 
//                closestPlayer.HeldItem.ModItem is BaseballBat modItem && 
//                modItem.canReflect &&
//                closestPlayer.itemAnimation > 0 && 
//                closestDistance < 80 &&
//                CanBeReflected(projectile) &&
//                !reflected)
//            {
//                ReflectByItem(projectile);

//                modItem.totalBouncedDamage += projectile.damage;
//                if (modItem.totalBouncedDamage > 200)
//                {
//                    modItem.canReflect = false;
//                }
//            }
//        }

//        private void ReflectByItem(Projectile projectile)
//        {
//            SoundEngine.PlaySound(SoundID.Item150, projectile.position);
//            for (int i = 0; i < 3; i++)
//            {
//                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke);
//                Main.dust[num].velocity *= 0.3f;
//            }

//            reflected = true;
//            projectile.hostile = false;
//            projectile.friendly = true;

//            Vector2 vector = -projectile.oldVelocity;
//            float length = projectile.oldVelocity.Length();

//            Vector2 randomVelocity = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
//            randomVelocity.Normalize();
//            randomVelocity *= length;

//            projectile.velocity = randomVelocity + vector * 20f;
//            projectile.velocity.Normalize();
//            projectile.velocity *= length;
//            projectile.damage *= 5;
//        }

//        public static bool CanBeReflected(Projectile projectile)
//        {
//            if (projectile.active && !projectile.friendly && projectile.hostile && projectile.damage > 0)
//            {
//                if (projectile.aiStyle is 1 or 2 or 8 or 10 or 12 or 14 or 16 or 18 or 21 or 24 or 27 or 28 or 29 or 37 or 50 or 51 or 131)
//                {
//                    return true;
//                }
//            }

//            return false;
//        }
//    }
//}
