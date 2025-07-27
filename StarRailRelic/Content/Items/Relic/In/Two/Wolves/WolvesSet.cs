namespace StarRailRelic.Content.Items.Relic.In.Two.Wolves
{
    public abstract class WolvesSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Wolves;

        public override void UpdateRelicSetTwo(Player player)
        {
            int summonWeaponCount = 0;

            Player[] players = player.GetAllTeamPlayers();

            foreach (Player p in players)
            {
                if (p.active && p.HeldItem.summon)
                {
                    summonWeaponCount++;
                }
            }

            if (summonWeaponCount >= 2)
            {
                foreach (Player p in players)
                {
                    p.maxMinions += 1;
                }
            }
        }
    }

    public class WolvesRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Wolves;
    }

    public class WolvesSphere : WolvesSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class WolvesRope : WolvesSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
