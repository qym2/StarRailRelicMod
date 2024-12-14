using StarRailRelic.Content.Items.Weapons.Melee;

namespace StarRailRelic.Common.Players
{
    public class BaseballBatReflectPlayer : ModPlayer
    {
        public override void UpdateEquips()
        {
            Player.ItemCheck_GetMeleeHitbox(Player.HeldItem, Item.GetDrawHitbox(((Player.itemAnimation > 0) ? Player.lastVisualizedSelectedItem : Player.HeldItem).type, Player), out _, out Rectangle itemRectangle);

            if (Player.HeldItem.type == ItemType<BaseballBat>() && BaseballBat.CanReflect && Player.itemAnimation > 0)
            {
                foreach (Projectile projectile in Main.projectile)
                {
                    if (CanBeReflected(projectile) &&
                        itemRectangle.Intersects(projectile.Hitbox))
                    {
                        ReflectByItem(projectile);

                        BaseballBat.TotalBouncedDamage += projectile.damage;
                        if (BaseballBat.TotalBouncedDamage > 200)
                        {
                            BaseballBat.CanReflect = false;
                            break;
                        }
                    }
                }
            }
        }

        private static void ReflectByItem(Projectile projectile)
        {
            SoundEngine.PlaySound(SoundID.Item150, projectile.position);
            for (int i = 0; i < 3; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke);
                Main.dust[num].velocity *= 0.3f;
            }

            projectile.hostile = false;
            projectile.friendly = true;

            Vector2 vector = -projectile.oldVelocity;
            float length = projectile.oldVelocity.Length();

            Vector2 randomVelocity = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
            randomVelocity.Normalize();
            randomVelocity *= length;

            projectile.velocity = randomVelocity + vector * 20f;
            projectile.velocity.Normalize();
            projectile.velocity *= length;
            projectile.damage *= 5;
        }

        private static bool CanBeReflected(Projectile projectile)
        {
            if (projectile.active && !projectile.friendly && projectile.hostile && projectile.damage > 0)
            {
                if (projectile.aiStyle is 1 or 2 or 8 or 10 or 12 or 14 or 16 or 18 or 21 or 24 or 27 or 28 or 29 or 37 or 50 or 51 or 131)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
