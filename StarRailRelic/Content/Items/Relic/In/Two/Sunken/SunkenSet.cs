namespace StarRailRelic.Content.Items.Relic.In.Two.Sunken
{
    public abstract class SunkenSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Sunken;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetAttackSpeed<RangedDamageClass>() += 12 / 100f;

            int rangedWeaponCount = 0;

            Player[] players = player.GetAllTeamPlayers();

            foreach (Player p in players)
            {
                if (p.active && p.HeldItem.ranged)
                {
                    rangedWeaponCount++;
                }
            }

            if (rangedWeaponCount >= 2)
            {
                foreach (Player p in players)
                {
                    p.GetAttackSpeed<RangedDamageClass>() += 6 / 100f;
                }
            }
        }
    }

    public class SunkenRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Sunken;
    }

    public class SunkenSphere : SunkenSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class SunkenRope : SunkenSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
