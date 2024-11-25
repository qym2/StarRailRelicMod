namespace StarRailRelic.Content.Items.Relic.In.One.Xianzhou
{
    public abstract class XianzhouSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Xianzhou;
    }

    public class XianzhouSphere : XianzhouSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class XianzhouRope : XianzhouSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
