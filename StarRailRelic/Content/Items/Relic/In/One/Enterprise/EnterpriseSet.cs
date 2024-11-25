namespace StarRailRelic.Content.Items.Relic.In.One.Enterprise
{
    public abstract class EnterpriseSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Enterprise;
    }

    public class EnterpriseSphere : EnterpriseSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class EnterpriseRope : EnterpriseSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
