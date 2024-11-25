namespace StarRailRelic.Content.Items.Relic.In.One.Penacony
{
    public abstract class PenaconySet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Penacony;
    }

    public class PenaconySphere : PenaconySet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class PenaconyRope : PenaconySet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
