namespace StarRailRelic.Content.Items.Relic.In.One.Differentiator
{
    public abstract class DifferentiatorSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Differentiator;
    }

    public class DifferentiatorSphere : DifferentiatorSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class DifferentiatorRope : DifferentiatorSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
