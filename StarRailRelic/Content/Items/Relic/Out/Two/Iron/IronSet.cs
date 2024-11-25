namespace StarRailRelic.Content.Items.Relic.Out.Two.Iron
{
    public abstract class IronSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Iron;
    }

    public class IronHead : IronSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class IronHands : IronSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class IronBody : IronSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class IronFeet : IronSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
