namespace StarRailRelic.Content.Items.Relic.In.One.Penacony
{
    public abstract class PenaconySet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Penacony;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.manaCost -= 12f / 100f;

            int magicWeaponCount = 0;

            Player[] players = player.GetAllTeamPlayers();

            foreach (Player p in players)
            {
                if (p.active && p.HeldItem.magic)
                {
                    magicWeaponCount++;
                }
            }

            if (magicWeaponCount >= 2)
            {
                foreach (Player p in players)
                {
                    p.manaCost -= 6f / 100f;
                }
            }
        }
    }

    public class PenaconyRelic : InRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Penacony;
    }

    public class PenaconySphere : PenaconySet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class PenaconyRope : PenaconySet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
