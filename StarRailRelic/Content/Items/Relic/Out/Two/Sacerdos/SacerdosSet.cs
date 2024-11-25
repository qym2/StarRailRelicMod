namespace StarRailRelic.Content.Items.Relic.Out.Two.Sacerdos
{
    public abstract class SacerdosSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Sacerdos;
    }

    public class SacerdosHead : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Head;
    }

    public class SacerdosHands : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Hands;
    }

    public class SacerdosBody : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Body;
    }

    public class SacerdosFeet : SacerdosSet
    {
        public override RelicType RelicType => RelicType.Feet;
    }
}
