namespace StarRailRelic.Content.Items.Relic.Out.Two.Feixiao
{
    public abstract class FeixiaoSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Feixiao;
    }

    public class FeixiaoHead : FeixiaoSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class FeixiaoHands : FeixiaoSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class FeixiaoBody : FeixiaoSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class FeixiaoFeet : FeixiaoSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
