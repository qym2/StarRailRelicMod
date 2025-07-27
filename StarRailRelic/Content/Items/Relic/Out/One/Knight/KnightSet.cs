namespace StarRailRelic.Content.Items.Relic.Out.One.Knight
{
    public abstract class KnightSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Knight;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.statDefense *= 1.1f;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.hasPaladinShield = true;

            Player[] players = player.GetTeammates();
            foreach (Player p in players)
            {
                p.AddBuff(BuffID.PaladinsShield, 20);
            }

            if(players.Length <= 0)
            {
                player.endurance += 0.06f;
            }
        }
    }

    public class KnightRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Knight;
    }

    public class KnightHead : KnightSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class KnightHands : KnightSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class KnightBody : KnightSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class KnightFeet : KnightSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
