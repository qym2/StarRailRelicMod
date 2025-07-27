namespace StarRailRelic.Content.Items.Relic.In.Two.Izumo
{
    public abstract class IzumoSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Izumo;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<MeleeDamageClass>() += 12 / 100f;

            int meleeWeaponCount = 0;

            Player[] players = player.GetAllTeamPlayers();

            foreach (Player p in players)
            {
                if (p.active && p.HeldItem.melee)
                {
                    meleeWeaponCount++;
                }
            }

            if (meleeWeaponCount >= 2)
            {
                foreach (Player p in players)
                {
                    p.GetDamage<MeleeDamageClass>() += 6 / 100f;
                }
            }
        }
    }

    public class IzumoRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Izumo;
    }

    public class IzumoSphere : IzumoSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class IzumoRope : IzumoSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
