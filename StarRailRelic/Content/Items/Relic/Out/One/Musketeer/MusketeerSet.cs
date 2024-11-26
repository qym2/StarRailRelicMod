namespace StarRailRelic.Content.Items.Relic.Out.One.Musketeer
{
    public abstract class MusketeerSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Musketeer;

        public override void UpdateRelicSetTwo(Player player)
        {
            player.GetDamage<GenericDamageClass>() += 4f / 100f;
        }

        public override void UpdateRelicSetFour(Player player)
        {
            player.moveSpeed += 0.12f;
            player.GetDamage<MeleeDamageClass>() += 4f / 100f;
        }
    }

    public class MusketeerHead : MusketeerSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class MusketeerHands : MusketeerSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class MusketeerBody : MusketeerSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class MusketeerFeet : MusketeerSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
