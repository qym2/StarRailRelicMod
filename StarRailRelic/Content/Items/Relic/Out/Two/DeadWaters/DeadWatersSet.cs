namespace StarRailRelic.Content.Items.Relic.Out.Two.DeadWaters
{
    public abstract class DeadWatersSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.DeadWaters;
    }

    public class DeadWatersHead : DeadWatersSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class DeadWatersHands : DeadWatersSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class DeadWatersBody : DeadWatersSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class DeadWatersFeet : DeadWatersSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
