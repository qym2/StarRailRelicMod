namespace StarRailRelic.Content.Items.Relic.Out.Two.Watch
{
    public abstract class WatchSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Watch;
    }

    public class WatchHead : WatchSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class WatchHands : WatchSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class WatchBody : WatchSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class WatchFeet : WatchSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
