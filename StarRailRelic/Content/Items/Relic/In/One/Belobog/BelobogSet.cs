namespace StarRailRelic.Content.Items.Relic.In.One.Belobog
{
    public abstract class BelobogSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Belobog;
    }

    public class BelobogSphere : BelobogSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class BelobogRope : BelobogSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
