namespace StarRailRelic.Content.Items.Relic.Out.Two.Dot
{
    public abstract class DotSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Dot;
    }

    public class DotHead : DotSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class DotHands : DotSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class DotBody : DotSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class DotFeet : DotSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
