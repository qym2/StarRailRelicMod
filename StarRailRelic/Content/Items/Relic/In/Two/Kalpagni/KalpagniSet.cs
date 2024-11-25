namespace StarRailRelic.Content.Items.Relic.In.Two.Kalpagni
{
    public abstract class KalpagniSet : ModRelic
    {
        public override RelicSet RelicSet => RelicSet.Kalpagni;
    }

    public class KalpagniSphere : KalpagniSet
    {
        public override RelicType RelicType => RelicType.PlanarSphere;
    }

    public class KalpagniRope : KalpagniSet
    {
        public override RelicType RelicType => RelicType.LinkRope;
    }
}
