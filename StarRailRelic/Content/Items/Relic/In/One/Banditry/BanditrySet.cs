namespace StarRailRelic.Content.Items.Relic.In.One.Banditry
{
    public abstract class BanditrySet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Banditry;
    }

    public class BanditrySphere : BanditrySet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class BanditryRope : BanditrySet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
