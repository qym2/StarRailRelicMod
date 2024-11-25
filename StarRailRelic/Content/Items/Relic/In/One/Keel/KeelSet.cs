namespace StarRailRelic.Content.Items.Relic.In.One.Keel
{
    public abstract class KeelSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Keel;
    }

    public class KeelSphere : KeelSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class KeelRope : KeelSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
