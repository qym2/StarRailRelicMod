namespace StarRailRelic.Content.Items.Relic.In.Two.Wolves
{
    public abstract class WolvesSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Wolves;
    }

    public class WolvesSphere : WolvesSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class WolvesRope : WolvesSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
