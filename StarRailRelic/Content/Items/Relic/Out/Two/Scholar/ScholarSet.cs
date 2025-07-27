namespace StarRailRelic.Content.Items.Relic.Out.Two.Scholar
{
    public abstract class ScholarSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Scholar;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetCritChance<GenericDamageClass>() += 4;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.GetDamage<SummonDamageClass>() += 10 / 100f;

            if(player.velocity.Length() > 0)
            {
                player.GetDamage<SummonDamageClass>() += 10 / 100f;
            }
        }
    }

    public class ScholarRelic : OutRelicBag
    {
        public override RelicSet RelicSet => RelicSet.Scholar;
    }

    public class ScholarHead : ScholarSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class ScholarHands : ScholarSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class ScholarBody : ScholarSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class ScholarFeet : ScholarSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
