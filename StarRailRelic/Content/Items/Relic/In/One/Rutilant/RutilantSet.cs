namespace StarRailRelic.Content.Items.Relic.In.One.Rutilant
{
    public abstract class RutilantSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Rutilant;
    }

    public class RutilantSphere : RutilantSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class RutilantRope : RutilantSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
