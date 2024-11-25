namespace StarRailRelic.Content.Items.Relic.In.One.Vonwacq
{
    public abstract class VonwacqSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Vonwacq;
    }

    public class VonwacqSphere : VonwacqSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class VonwacqRope : VonwacqSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
